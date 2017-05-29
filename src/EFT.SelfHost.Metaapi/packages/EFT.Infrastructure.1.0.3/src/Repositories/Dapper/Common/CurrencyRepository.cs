using EFT.Core.Repositories.Dapper;
using EFT.Domain.Entities.Common;
using EFT.Infrastructure.Repositories.Interfaces.Common;
namespace EFT.Infrastructure.Repositories.Dapper.Common
{
    public class CurrencyRepository : DapperRepositoryBase<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(DapperHelper dapperHelper) : base(dapperHelper)
        {
        }
    }
}
