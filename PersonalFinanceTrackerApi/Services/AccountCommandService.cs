using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PersonalFinanceTrackerApi.Contracts;
using PersonalFinanceTrackerApi.DTOs;
using PersonalFinanceTrackerApi.Entities;
using PersonalFinanceTrackerApi.Exceptions;
using PersonalFinanceTrackerApi.Persistence;

namespace PersonalFinanceTrackerApi.Services
{
    public class AccountCommandService : IAccountCommandService
    {
        private readonly FinanceDbContext _context;

        public AccountCommandService(FinanceDbContext context)
        {
            _context = context;
        }
        public async Task<Account> CreateAsync(CreateAccountDto dto)
        {
            var exists = await _context.Accounts.AnyAsync(a => a.AccountName == dto.AccountName);
            if (exists)
            {
                throw new ConflictException("There is already an account by that name");
            }

            var typeExists = await _context.AccountTypes.AnyAsync(at => at.Id == dto.AccountTypeId);
            if(!typeExists)
            {
                throw new NotFoundException("Account type by that id does not exist");
            }

            var account = new Account
            {
                AccountName = dto.AccountName,
                AccountTypeId = dto.AccountTypeId,
                CurrentBalance = dto.CurrentBalance
            };

            _context.Add(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<bool> UpdateAsync(int id, UpdateAccountDto dto)
        {
            var existingAccount = await _context.Accounts.FindAsync(id);
            if(existingAccount == null)
            {
                throw new NotFoundException("Account not found");
            }

            var accountTypeExists = await _context.AccountTypes.AnyAsync(at => at.Id == dto.AccountTypeId);
            if(!accountTypeExists)
            {
                throw new NotFoundException("Account Type by that id does not exist.");
            }

            var duplicateAccount = await _context.Accounts.AnyAsync(a => a.AccountName == dto.AccountName && a.Id != id);
            if(duplicateAccount)
            {
                throw new ConflictException("Account by this name already exists");
            }

            existingAccount.AccountName = dto.AccountName;
            existingAccount.AccountTypeId = dto.AccountTypeId;
            existingAccount.CurrentBalance = dto.CurrentBalance;

           
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var accountToDelete = await _context.Accounts.FindAsync(id);
            if(accountToDelete == null)
            {
                throw new NotFoundException("Account not found to delete");
            }

            _context.Remove(accountToDelete);
            await _context.SaveChangesAsync();

            return true;
        }

      
    }
}
