using EFT.Core.Repositories.Dapper;
using EFT.Domain.Entities.BankTrans;
using EFT.Infrastructure.Repositories.Interfaces.BankTrans;
namespace EFT.Infrastructure.Repositories.Dapper.BankTrans
{
    public class StandardResponseRepository : DapperRepositoryBase<StandardResponse>, IStandardResponseRepository
    {
        public StandardResponseRepository(DapperHelper dappperHelper) : base(dappperHelper)
        {
        }
    }
}
