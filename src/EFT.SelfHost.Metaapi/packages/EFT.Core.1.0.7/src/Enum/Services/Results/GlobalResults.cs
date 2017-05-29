using EFT.Core.Services.Errors.Consts;
using EFT.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Core.Services.Results
{
    public static class GlobalResults
    {
        public static IServiceResult BodyRequired()
        {
            return new ServiceResult(GlobalErrors.InvalidRequest, "Please submit a request in json format and with the desired attributes");
        }
        public static IServiceResult NoRecord()
        {
            return new ServiceResult(GlobalErrors.NotFound, "There are no record available");
        }
    }
}
