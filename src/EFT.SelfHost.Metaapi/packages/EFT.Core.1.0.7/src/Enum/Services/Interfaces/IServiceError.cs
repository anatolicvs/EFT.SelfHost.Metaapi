using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Core.Services.Interfaces
{
    public interface IServiceError
    {
        int Code { get; }
        string Description { get; }
        object Data { get; set; }
        Exception Exception { get; }
    }
}
