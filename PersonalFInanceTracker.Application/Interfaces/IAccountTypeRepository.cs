
using PersonalFinanceTracker.Domain.Entities;

namespace PersonalFinanceTracker.Application.Interfaces
{
    public interface IAccountTypeRepository
    {
        // Query
        Task<IEnumerable<AccountType>> GetAllAccountTypesAsync();
        Task<AccountType?> GetAccountTypeByIdAsync(int id);


        // Command
        Task<bool> ExistsByNameAsync(string accountTypeName);
        Task<bool> AccountTypeExistsAsync(int accountTypeId);
        Task<AccountType> CreateAsync(AccountType accountType);

        Task<bool> UpdateAsync(AccountType accountType);
        Task<bool> DeactivateAsync(AccountType accountType);

    }

}