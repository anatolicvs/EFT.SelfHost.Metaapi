using EFT.Core.Repositories.Dapper;
using EFT.Core.Services.Interfaces;
using EFT.Domain.Dto.Customer;
using EFT.Domain.Entities.Customer;
using EFT.Infrastructure.Repositories.Interfaces.Customer;
using System;
using System.Threading.Tasks;
namespace EFT.Infrastructure.Repositories.Dapper.Customer
{
    public class BankAccountRepository : DapperRepositoryBase<BankAccount>, IBankAccount
    {
        public BankAccountRepository(DapperHelper dapperHelper) : base(dapperHelper)
        {
        }

        public async Task<IServicePagedList<BankAccountDto>> GetBankAccountsAsync(int pageIndex, int pageSize, string userId)
        {
            try
            {
                string query = $@"SELECT 
                                         b.ID as BankId,
                                         c.Name as Currency,
                                         b.Number as Number, 
                                         bn.Name as BankName FROM Customer.BankAccount b 
                                         INNER JOIN Common.Bank bn on b.BankId = bn.ID 
                                         INNER JOIN Common.Currency c on b.CurrencyId = c.ID 
                                         WHERE b.UserId=@UserId Order by c.Name desc";

                return await  _dapperHelper.Connection.GetServicePagedListAsync<BankAccountDto>(query,pageIndex,pageSize, new { UserId = userId } );
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
