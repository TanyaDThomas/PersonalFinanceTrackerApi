using PersonalFinanceTrackerApi.DTOs;
using PersonalFinanceTrackerApi.Entities;

namespace PersonalFinanceTrackerApi.Contracts
{
    public interface IAccountCommandService
    {
        Task<Account> CreateAsync(CreateAccountDto dto);
        Task<bool> UpdateAsync(int id, UpdateAccountDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
