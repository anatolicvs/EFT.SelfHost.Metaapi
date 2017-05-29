using EFT.Core.Repositories.Dapper;
using EFT.Domain.Entities.BankTrans;
using EFT.Infrastructure.Repositories.Interfaces.BankTrans;
namespace EFT.Infrastructure.Repositories.Dapper.BankTrans
{
    public class AcquirerReferenceRepository : DapperRepositoryBase<AcquirerReference>, IAcquirerReferenceRepository
    {
        public AcquirerReferenceRepository(DapperHelper dapperHelper) : base(dapperHelper)
        {
        }
    }
}
