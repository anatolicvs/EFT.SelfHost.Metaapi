using EFT.Core.Extensions;
using EFT.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Core.Services.Extensions
{
    public static class ServiceResultExtensions
    {
        public static IServiceResult<TResult> ToResult<TResult>(this IServiceResult serviceResult)
        {
            var result = new ServiceResult<TResult>();
            result.Errors.AddRange(serviceResult.Errors);
            return result;
        }
        public static IServiceResult<TResult> ToResult<TResult>(this IServiceResult serviceResult, TResult result)
        {
            var resultTo = new ServiceResult<TResult>(result);
            resultTo.Errors.AddRange(serviceResult.Errors);
            return resultTo;
        }
        public static Task<IServiceResult<TResult>> ToResultAsync<TResult>(this IServiceResult serviceResult)
        {
            return Task.FromResult(ToResult<TResult>(serviceResult));
        }
        public static Task<IServiceResult<TResult>> ToResultAsync<TResult>(this IServiceResult serviceResult, TResult result)
        {
            return Task.FromResult(ToResult<TResult>(serviceResult, result));
        }
        public static IServiceResult<IServicePagedList<TResult>> ToPagedListResult<TResult>(this IServicePagedList<TResult> list) where TResult : class, new()
        {
            return new ServiceResult<IServicePagedList<TResult>>(list);
        }
        public static IServiceResult<IServicePagedList<TResult>> ToPagedListResult<TResult>(this IEnumerable<TResult> list, int pageIndex, int pageSize, long itemCount) where TResult : class, new()
        {
            return new ServiceResult<IServicePagedList<TResult>>(list.ToPagedList(pageIndex, pageSize, itemCount));
        }
    }
}
