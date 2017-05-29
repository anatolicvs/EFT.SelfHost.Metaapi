using EFT.Core.Repositories;
using EFT.Core.Services.Interfaces;
using EFT.Domain.Dto.Customer;
using EFT.Domain.Entities.Customer;
using System.Threading.Tasks;
namespace EFT.Infrastructure.Repositories.Interfaces.Customer
{
    public interface IBankAccount : IRepository<BankAccount>
    {
        Task<IServicePagedList<BankAccountDto>> GetBankAccountsAsync(int pageIndex, int pageSize, string userId);
    }
}
