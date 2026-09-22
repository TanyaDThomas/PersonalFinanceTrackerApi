using PersonalFinanceTracker.Application.DTOs;
using PersonalFinanceTracker.Domain.Entities;


namespace PersonalFinanceTracker.Application.Interfaces
{
    public interface IAccountRepository
    {
        // Query
        Task<IEnumerable<Account>> GetAllAccountsAsync(string? accountTypeName, bool? isActive, string? accountName);
        Task<Account?> GetAccountByIdAsync(int id);

        // Command
        Task<bool> ExistsByNameAsync(string accountName);
        Task<bool> AccountTypeExistsAsync(int accountTypeId);
        Task<Account> CreateAsync(Account account);


        Task<bool> UpdateAsync(Account account);
        Task<bool> DeactivateAsync(Account account);
    }
}
