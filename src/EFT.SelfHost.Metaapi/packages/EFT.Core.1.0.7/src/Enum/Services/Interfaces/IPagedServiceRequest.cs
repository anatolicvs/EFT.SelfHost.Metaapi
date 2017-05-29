using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Core.Services.Interfaces
{
    public interface IPagedServiceRequest
    {
        int? PageSize { get; set; }
        int? PageIndex { get; set; }
    }
}
