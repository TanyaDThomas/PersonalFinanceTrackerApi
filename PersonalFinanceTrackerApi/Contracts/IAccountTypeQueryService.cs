using PersonalFinanceTrackerApi.Entities;

namespace PersonalFinanceTrackerApi.Contracts
{
    public interface IAccountTypeQueryService
    {
        Task<IEnumerable<AccountType>> GetAllAsync();
        Task<AccountType> GetTypeByIdAsync(int id);
    }
}
