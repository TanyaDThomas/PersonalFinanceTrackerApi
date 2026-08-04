using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTrackerApi.Contracts;
using PersonalFinanceTrackerApi.DTOs;
using PersonalFinanceTrackerApi.Entities;
using PersonalFinanceTrackerApi.Exceptions;
using PersonalFinanceTrackerApi.Persistence;

namespace PersonalFinanceTrackerApi.Services
{
    public class AccountTypeCommandService : IAccountTypeCommandService
    {
        private readonly FinanceDbContext _context;

        public AccountTypeCommandService(FinanceDbContext context)
        {
            _context = context;
        }
        public async Task<AccountType> CreateAsync(CreateAccountTypeDto dto)
        {
            var exists = await _context.AccountTypes.AnyAsync(a => a.Name == dto.AccountTypeName);
            if (exists)
            {
                throw new ConflictException("There is already an account type by that name");
            }

            var accountType = new AccountType
            {
                Name = dto.AccountTypeName
            };

            _context.Add(accountType);
            await _context.SaveChangesAsync();

            return accountType;
           
        }

        public async Task<bool> UpdateAsync(int id, UpdateAccountTypeDto dto)
        {
            var existingType = await _context.AccountTypes.FindAsync(id);
            if(existingType ==null)
            {
                throw new NotFoundException("Account type not found.");
            }

            var duplicateExists = await _context.AccountTypes.AnyAsync(at => at.Name == dto.AccountTypeName && at.Id != id);
            if(duplicateExists)
            {
                throw new ConflictException("Account Type already exists");
            }

            existingType.Name = dto.AccountTypeName;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var typeToDelete = await _context.AccountTypes.FindAsync(id);
            if(typeToDelete == null)
            {
                throw new NotFoundException("Account type does not exist.");
            }

            _context.Remove(typeToDelete);
            await _context.SaveChangesAsync();

            return true;
            
        }

    }
}
