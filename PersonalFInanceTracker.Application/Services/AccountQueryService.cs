
using PersonalFinanceTracker.Application.Services.Contracts;
using PersonalFinanceTracker.Application.DTOs;
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Application.Exceptions;
using PersonalFinanceTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PersonalFinanceTracker.Application.Services
{
    public class AccountQueryService : IAccountQueryService
    {
        private readonly FinanceDbContext _context;

        public AccountQueryService(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync(string? accountTypeName,bool? isActive, string? accountName)
        {
            var query = _context.Accounts.AsQueryable();

           if(!string.IsNullOrWhiteSpace(accountTypeName))
            {
                query = query.Where(a => a.AccountType.Name.Contains(accountTypeName));
            }

            if(isActive.HasValue)
            {
                query = query.Where(a => a.IsActive == isActive.Value);
            }

            if(!string.IsNullOrWhiteSpace(accountName))
            {
                query = query.Where(a => a.AccountName.Contains(accountName));
            }

            return await query
                .AsNoTracking()
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
