using EFT.Core.Repositories.Dapper;
using EFT.Domain.Entities.BankTrans;
using EFT.Infrastructure.Repositories.Interfaces.BankTrans;
namespace EFT.Infrastructure.Repositories.Dapper.BankTrans
{
    public class PurchaseRepository : DapperRepositoryBase<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(DapperHelper dapperHelper) : base(dapperHelper)
        {
        }
    }
}
