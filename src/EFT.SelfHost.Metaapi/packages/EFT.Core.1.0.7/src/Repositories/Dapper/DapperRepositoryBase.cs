using Dapper;
using EFT.Core.Domains;
using EFT.Core.Helper;
using EFT.Core.Services;
using EFT.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFT.Core.Repositories.Dapper
{
    public abstract class DapperRepositoryBase<TEntity> : IRepository<TEntity>, IUnitofwork
        where TEntity : class, IEntity, new()
    {
        protected readonly DapperHelper _dapperHelper;
        protected DapperRepositoryBase(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public void BeginTransaction()
        {
            _dapperHelper.BeginTransaction();
        }
        public void Commit()
        {
            _dapperHelper.Commit();
        }
        public void Rollback()
        {
            _dapperHelper.Rollback();
        }

        public TEntity Add(TEntity entity)
        {
            try
            {
                var id = _dapperHelper.Connection.Insert(entity, transaction: _dapperHelper.Transaction) ?? 0;
                entity.GetType().GetProperty("ID").SetValue(entity, id);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEntity Update(TEntity entity)
        {
            _dapperHelper.Connection.Update(entity, transaction: _dapperHelper.Transaction);
            return entity;
        }
        public void Delete(TEntity entity)
        {
            _dapperHelper.Connection.Delete(entity, transaction: _dapperHelper.Transaction);
        }
        public void Delete(object whereConditions)
        {
            _dapperHelper.Connection.DeleteList<TEntity>(whereConditions, transaction: _dapperHelper.Transaction);
        }
        public TEntity Get(object id)
        {
            try
            {
                return _dapperHelper.Connection.GetList<TEntity>(new { ID = id }).SingleOrDefault();
            }
            catch
            {
                return null;
            }

        }
        public IEnumerable<TEntity> GetList(object whereConditions)
        {
            try
            {
                return _dapperHelper.Connection.GetList<TEntity>(whereConditions);
            }
            catch
            {
                return null;
            }
        }
        public IServicePagedList<TEntity> GetPagedList(string conditions)
        {
            throw new NotImplementedException();
        }
        public IServicePagedList<TEntity> GetPagedList(string conditions, string orderBy, int pageIndex, int pageSize, object parameters, bool includeItemCount)
        {
            var result = _dapperHelper.Connection.GetListPaged<TEntity>(pageIndex, pageSize, conditions, orderBy, parameters);

            if (includeItemCount)
                return new ServicePagedList<TEntity>(result, pageIndex, pageSize, _dapperHelper.Connection.RecordCount<TEntity>(conditions: conditions, parameters: parameters));
            return new ServicePagedList<TEntity>(result, pageIndex, pageSize);
        }
        public async Task<TEntity> GetAsync(object id)
        {
            try
            {
                return (await _dapperHelper.Connection.GetListAsync<TEntity>(new { ID = id })).SingleOrDefault();
            }
            catch
            {
                return null;
            }
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                var id = await _dapperHelper.Connection.InsertAsync(entity, transaction: _dapperHelper.Transaction) ?? 0;
                entity.GetType().GetProperty("ID").SetValue(entity, id);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await _dapperHelper.Connection.UpdateAsync(entity, transaction: _dapperHelper.Transaction);
            return entity;
        }
        public async Task<IEnumerable<TEntity>> GetListAsync(object whereConditions)
        {
            try
            {
                return await _dapperHelper.Connection.GetListAsync<TEntity>(whereConditions);
            }
            catch
            {
                return null;
            }
        }
        public async Task DeleteAsync(TEntity entity)
        {
            await _dapperHelper.Connection.DeleteAsync(entity, transaction: _dapperHelper.Transaction);
        }
    }
}
