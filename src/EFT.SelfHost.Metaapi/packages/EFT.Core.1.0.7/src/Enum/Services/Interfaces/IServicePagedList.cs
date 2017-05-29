using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Core.Services.Interfaces
{
    public interface IServicePagedList<T> : IList<T>
    {
        int PageIndex { get; }
        int PageSize { get; }
        long ItemCount { get; }
    }
}
