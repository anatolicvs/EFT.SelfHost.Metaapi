using EFT.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Core.Extensions
{
    /*
    public static class CacheStoreExtensions
    {
        public static IServiceResult<T> Get<T>(this ICacheStore source, string key, TimeSpan time, Func<T> fetch) where T : class
        {
            if (source.Exists(key))
                return new ServiceResult<T>(source.Get<T>(key));

            var result = fetch();
            if (result == null)
                return new ServiceResult(GlobalErrors.NotCached, "Object not cached yet").ToResult<T>();

            source.Set(key, result, time);
            return new ServiceResult<T>(result);
        }
    }*/
}
