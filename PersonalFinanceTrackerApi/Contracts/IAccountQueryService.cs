using PersonalFinanceTrackerApi.Entities;

namespace PersonalFinanceTrackerApi.Contracts
{
    public interface IAccountQueryService
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account> GetAccountsByIdAsync(int id);
    }
}
