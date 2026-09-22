using PersonalFinanceTracker.Application.Interfaces;
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PersonalFinanceTracker.Infrastructure.Repositories
{
    public class AccountTypeRepository : IAccountTypeRepository
    {
        private readonly FinanceDbContext _context;

        public AccountTypeRepository(FinanceDbContext context)
        {
            _context = context;
        }

        // Command

        public async Task<bool> ExistsByNameAsync(string accountTypeName)
        {
            return await _context.AccountTypes.AnyAsync(at => at.Name == accountTypeName);
        }
        public async Task<bool> AccountTypeExistsAsync(int accountTypeId)
        {
            return await _context.AccountTypes.AnyAsync(at => at.Id == accountTypeId);
            
        }

        public async Task<AccountType> CreateAsync(AccountType accountType)
        {
            _context.AccountTypes.Add(accountType);
            await _context.SaveChangesAsync();
            return accountType;
        }

        public async Task<bool> UpdateAsync(AccountType accountType)
        {
            _context.AccountTypes.Update(accountType);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateAsync(AccountType accountType)
        {
            accountType.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

     


        // Query

        public async Task<AccountType?> GetAccountTypeByIdAsync(int id)
        {
            return await _context.AccountTypes.FindAsync(id);
        }

        public async Task<IEnumerable<AccountType>> GetAllAccountTypesAsync()
        {
            return await _context.AccountTypes
              .AsNoTracking()
              .ToListAsync();
        }

       
    }
}
