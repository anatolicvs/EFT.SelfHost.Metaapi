using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using EFT.Core.Services.Interfaces;
using EFT.Core.Services;
using System.Text.RegularExpressions;

namespace EFT.Core.Repositories.Dapper
{
    public static class DapperExtensions
    {
        #region Master-Detail
        public static IEnumerable<TParent> QueryParentChild<TParent, TChild, TParentKey>(this IDbConnection connection, string sql,
            Func<TParent, TParentKey> parentKeySelector, Func<TParent, IList<TChild>> childSelector,
            dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            Dictionary<TParentKey, TParent> cache = new Dictionary<TParentKey, TParent>();

            connection.Query<TParent, TChild, TParent>(
                sql,
                (parent, child) =>
                {
                    if (!cache.ContainsKey(parentKeySelector(parent)))
                    {
                        cache.Add(parentKeySelector(parent), parent);
                    }

                    TParent cachedParent = cache[parentKeySelector(parent)];
                    IList<TChild> children = childSelector(cachedParent);
                    children.Add(child);
                    return cachedParent;
                },
                param as object, transaction, buffered, splitOn, commandTimeout, commandType);

            return cache.Values;
        }
        public async static Task<IEnumerable<TParent>> QueryParentChildAsync<TParent, TChild, TParentKey>(this IDbConnection connection, string sql,
            Func<TParent, TParentKey> parentKeySelector, Func<TParent, IList<TChild>> childSelector,
            dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            Dictionary<TParentKey, TParent> cache = new Dictionary<TParentKey, TParent>();

            await connection.QueryAsync<TParent, TChild, TParent>(
                sql,
                (parent, child) =>
                {
                    if (!cache.ContainsKey(parentKeySelector(parent)))
                    {
                        cache.Add(parentKeySelector(parent), parent);
                    }

                    TParent cachedParent = cache[parentKeySelector(parent)];
                    IList<TChild> children = childSelector(cachedParent);
                    children.Add(child);
                    return cachedParent;
                },
                param as object, transaction, buffered, splitOn, commandTimeout, commandType);

            return cache.Values;
        }
        #endregion

        #region Paged List
        public static IServicePagedList<T> GetServicePagedList<T>(this IDbConnection con, string sql, int pageIndex = 1, int pageSize = 10, object param = null, bool includeItemCount = false)
        where T : class, new()
        {
            if (pageIndex <= 0 || pageSize <= 0)
                throw new Exception("'PageIndex' or 'PageSize' can not be equal or less than zero");

            if (!sql.ToUpper().Contains("ORDER BY"))
                throw new Exception("'Order By' keyword is not contains in query");

            int? itemCount = default(int);
            if (includeItemCount)
                itemCount = con.ExecuteScalar<int?>(sql.ToCountSql(), param);

            sql = sql.ToPagedSql(pageIndex, pageSize);
            return new ServicePagedList<T>(con.Query<T>(sql, param), pageIndex, pageSize, itemCount ?? 0);
        }
        public static async Task<IServicePagedList<T>> GetServicePagedListAsync<T>(this IDbConnection con, string sql, int pageIndex = 1, int pageSize = 10, object param = null, bool includeItemCount = false)
        where T : class, new()
        {
            if (pageIndex <= 0 || pageSize <= 0)
                throw new Exception("'PageIndex' or 'PageSize' can not be equal or less than zero");

            if (!sql.ToUpper().Contains("ORDER BY"))
                throw new Exception("'Order By' keyword is not contains in query");

            int? itemCount = default(int);
            if (includeItemCount)
                itemCount = await con.ExecuteScalarAsync<int?>(sql.ToCountSql(), param);

            sql = sql.ToPagedSql(pageIndex, pageSize);
            return new ServicePagedList<T>(await con.QueryAsync<T>(sql, param), pageIndex, pageSize, itemCount ?? 0);
        }
        private static string ToPagedSql(this string sql, int pageIndex, int pageSize)
        {
            var sqlText = $@"{sql} OFFSET {(pageIndex - 1) * pageIndex} ROWS
                             FETCH NEXT {pageSize} ROWS ONLY";
            return sqlText;
        }
        private static string ToCountSql(this string sql)
        {
            var countSql = Regex.Split(sql, "Order By", RegexOptions.IgnoreCase);
            return $@"SELECT Count(*) FROM ({countSql.First()}) baseQuery";
        }
        #endregion
    }
}
