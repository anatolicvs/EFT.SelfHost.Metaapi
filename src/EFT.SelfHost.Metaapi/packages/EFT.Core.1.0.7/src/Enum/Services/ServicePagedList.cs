using EFT.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Core.Services
{
    public class ServicePagedList<T> : List<T>, IServicePagedList<T>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public long ItemCount { get; private set; }
        public ServicePagedList(IEnumerable<T> source, int pageIndex, int pageSize, long itemCount)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            ItemCount = itemCount;
            AddRange(source);
        }
        public ServicePagedList(IEnumerable<T> source, int pageIndex, int pageSize) : this(source, pageIndex, pageSize, 0)
        {
        }
    }
}