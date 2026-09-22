
using Microsoft.EntityFrameworkCore;

using PersonalFinanceTracker.Application.Interfaces;
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Infrastructure.Persistence;

namespace PersonalFinanceTracker.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FinanceDbContext _context;
        public AccountRepository(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByNameAsync(string accountName)
        {
            return await _context.Accounts.AnyAsync(a => a.AccountName == accountName);
        }

        public async Task<bool> AccountTypeExistsAsync(int accountTypeId)
        {
            return await _context.Accounts.AnyAsync(at => at.Id == accountTypeId);
        }
        public async Task<Account> CreateAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<bool> UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateAsync(Account account)
        {
            account.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
            
        }

        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync(string? accountTypeName, bool? isActive, string? accountName)
        {
            var query = _context.Accounts
                .Include(a => a.AccountType)
                .AsQueryable();

            if(!string.IsNullOrWhiteSpace(accountTypeName))
            {
                query = query.Where(at => at.AccountType.Name.Contains( accountTypeName));
            }

            if(isActive.HasValue)
            {
                query = query.Where(a => a.IsActive == isActive.Value);
            }

            if(!string.IsNullOrWhiteSpace(accountName))
            {
                query = query.Where(a => a.AccountName.Contains(accountName));
            }

            return await query.ToListAsync();
        }

    
    }
}
