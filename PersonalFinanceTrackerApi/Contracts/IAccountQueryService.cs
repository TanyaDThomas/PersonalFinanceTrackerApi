using PersonalFinanceTrackerApi.DTOs;
using PersonalFinanceTrackerApi.Entities;

namespace PersonalFinanceTrackerApi.Contracts
{
    public interface IAccountQueryService
    {
        Task<IEnumerable<AccountDto>> GetAllAccountsAsync(string? accountTypeName, bool? isActive, string? accountName);
        Task<AccountDto> GetAccountsByIdAsync(int id);
    }
}
