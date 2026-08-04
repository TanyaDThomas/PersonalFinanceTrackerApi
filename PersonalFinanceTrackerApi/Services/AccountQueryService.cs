using Microsoft.EntityFrameworkCore;
using PersonalFinanceTrackerApi.Contracts;
using PersonalFinanceTrackerApi.DTOs;
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

        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
        {
            return await _context.Accounts
                .AsNoTracking()
                .Include(at => at.AccountType)
               .Select(a => new AccountDto
               {
                   Id = a.Id,
                   AccountName = a.AccountName,
                   AccountType = a.AccountType.Name,
                   CurrentBalance = a.CurrentBalance
               })
                .ToListAsync();
        }

        public async Task<AccountDto> GetAccountsByIdAsync(int id)
        {
            var accountById = await _context.Accounts
                .Include(at => at.AccountType)
                .Select(a => new AccountDto
                {
                    Id = a.Id,
                    AccountName = a.AccountName,
                    AccountType = a.AccountType.Name,
                    CurrentBalance = a.CurrentBalance
                })
                .FirstOrDefaultAsync( a => a.Id == id);

            if(accountById == null)
            {
                throw new NotFoundException("There is no account found by that id.");
            }

            return accountById;
        }

      
    }
}
