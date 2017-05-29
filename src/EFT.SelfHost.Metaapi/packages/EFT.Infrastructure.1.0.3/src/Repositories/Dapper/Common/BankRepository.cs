using EFT.Core.Repositories.Dapper;
using EFT.Domain.Entities.Common;
using EFT.Infrastructure.Repositories.Interfaces.Common;
namespace EFT.Infrastructure.Repositories.Dapper.Common
{
    public class BankRepository : DapperRepositoryBase<Bank>, IBankRepository
    {
        public BankRepository(DapperHelper dapperHelper) : base(dapperHelper)
        {
        }
    }
}
