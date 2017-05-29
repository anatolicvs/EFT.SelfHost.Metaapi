using EFT.Core.Services;
using EFT.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Core.Extensions
{
    public static class ListExtensions
    {
        public static IServicePagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            return new ServicePagedList<T>(source, pageIndex, pageSize);
        }
        public static IServicePagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, long itemCount)
        {
            return new ServicePagedList<T>(source, pageIndex, pageSize, itemCount);
        }
    }
}
