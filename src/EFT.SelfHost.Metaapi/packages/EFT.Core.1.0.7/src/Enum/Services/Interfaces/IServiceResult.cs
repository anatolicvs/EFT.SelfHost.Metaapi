using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Core.Services.Interfaces
{
    public interface IServiceResult
    {
        bool IsSuccess { get; }
        List<IServiceError> Errors { get; }
    }
    public interface IServiceResult<TResult> : IServiceResult
    {
        TResult Result { get; set; }
    }
}
