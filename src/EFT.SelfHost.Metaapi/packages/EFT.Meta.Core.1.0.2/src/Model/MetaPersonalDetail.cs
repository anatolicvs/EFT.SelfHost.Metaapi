using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Meta.Core.Model
{
   public  class MetaPersonalDetail
    {
        public DateTime Regdate { get; set; }
        public DateTime LastDate { get; set; }
        public int LastIp { get; set; }
        public double Balance { get; set; }
        public double PrevMonthBalance { get; set; }
        public double PrevBalance { get; set; }
        public double Credit { get; set; }
        public double InterestRate { get; set; }
        public double Taxes { get; set; }
        public double PrevMonthEquity { get; set; }
        public double PrevEquity { get; set; }
        public string Status { get; set; }     
        public string Id { get; set; }
        public string Email { get; set; }
        public int Login { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }      
    }
}
