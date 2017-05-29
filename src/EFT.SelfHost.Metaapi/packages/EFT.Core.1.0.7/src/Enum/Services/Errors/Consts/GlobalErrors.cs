using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Core.Services.Errors.Consts
{
    public class GlobalErrors
    {
        public const int NotFound = 101;
        public const int MaximumRequestLimit = 102;
        public const int InvalidRequest = 103;
        public const int ObjectUsing = 104;
        public const int NotAuthentication = 105;
        public const int NotAuthorization = 106;
        public const int NotDeleted = 107;
        public const int Passive = 108;
        public const int NotValidation = 109;
        public const int NotComplated = 110;
        public const int UnknownError = 111;
        public const int InsufficentBalance = 112;
        public const int NotCached = 113;
    }
}
