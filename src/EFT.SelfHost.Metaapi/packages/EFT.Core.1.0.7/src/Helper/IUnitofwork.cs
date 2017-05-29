using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Core.Helper
{
    public interface IUnitofwork
    {
        void BeginTransaction();
        void Rollback();
        void Commit();
    }
}
