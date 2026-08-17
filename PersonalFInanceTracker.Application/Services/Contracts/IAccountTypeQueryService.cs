using PersonalFinanceTracker.Domain.Entities;

namespace PersonalFinanceTracker.Application.Services.Contracts
{
    public interface IAccountTypeQueryService
    {
        Task<IEnumerable<AccountType>> GetAllAsync();
        Task<AccountType> GetTypeByIdAsync(int id);
    }
}
