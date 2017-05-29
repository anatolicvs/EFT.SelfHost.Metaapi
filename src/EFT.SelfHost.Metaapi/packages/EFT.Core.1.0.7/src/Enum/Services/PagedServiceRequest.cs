using EFT.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Core.Services
{
    public class PagedServiceRequest : IPagedServiceRequest
    {
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
    }
}
