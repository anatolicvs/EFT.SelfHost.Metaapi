using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Meta.Core.Model
{
    public class MetaAccount
    {
        public int AccountNo { get; set; }
        public string AccountType { get; set; }
        public string Currency { get; set; }
        public string AccountState { get; set; }
        public int Leverage { get; set; }
        public double Balance { get; set; }
        public double MarginLevel { get; set; }
        public double FreeMargin { get; set; }
        public bool isDemo { get; set; }
    }
}
 