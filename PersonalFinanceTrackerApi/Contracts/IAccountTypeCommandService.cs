using PersonalFinanceTrackerApi.DTOs;
using PersonalFinanceTrackerApi.Entities;

namespace PersonalFinanceTrackerApi.Contracts
{
    public interface IAccountTypeCommandService
    {
        Task<AccountType> CreateAsync(CreateAccountTypeDto dto);
        Task<bool> UpdateAsync(int id, UpdateAccountTypeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
