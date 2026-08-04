using Microsoft.EntityFrameworkCore;
using PersonalFinanceTrackerApi.Contracts;
using PersonalFinanceTrackerApi.Entities;
using PersonalFinanceTrackerApi.Exceptions;
using PersonalFinanceTrackerApi.Persistence;

namespace PersonalFinanceTrackerApi.Services
{
    public class AccountQueryService : IAccountQueryService
    {
        private readonly FinanceDbContext _context;

        public AccountQueryService(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Account> GetAccountsByIdAsync(int id)
        {
            var accountById = await _context.Accounts.FindAsync(id);
            if(accountById == null)
            {
                throw new NotFoundException("There is no account found by that id.");
            }

            return accountById;
        }

      
    }
}
