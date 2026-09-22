
using PersonalFinanceTracker.Application.DTOs;
using PersonalFinanceTracker.Application.Exceptions;
using PersonalFinanceTracker.Application.Interfaces;
using PersonalFinanceTracker.Application.Services.Contracts;

using PersonalFinanceTracker.Domain.Entities;

namespace PersonalFinanceTracker.Application.Services
{
    public class AccountCommandService : IAccountCommandService
    {
        private readonly IAccountRepository _repo;

        public AccountCommandService(IAccountRepository repo)
        {
            _repo = repo;
        }
        public async Task<Account> CreateAsync(CreateAccountDto dto)
        {
            var exists = await _repo.ExistsByNameAsync(dto.AccountName);
            if (exists)
            {
                throw new ConflictException("There is already an account by that name");
            }

            var typeExists = await _repo.AccountTypeExistsAsync(dto.AccountTypeId);
            if (!typeExists)
            {
                throw new NotFoundException("Account type by that id does not exist");
            }


            var account = new Account
            {
                AccountName = dto.AccountName,
                AccountTypeId = dto.AccountTypeId,
                CurrentBalance = dto.CurrentBalance
            };

            await _repo.CreateAsync(account);
            return account;
            
        }

        public async Task<bool> UpdateAsync(int id, UpdateAccountDto dto)
        {
            var existingAccount = await _repo.GetAccountByIdAsync(id);
            if (existingAccount == null)
            {
                throw new NotFoundException("Account not found");
            }

            var accountTypeExists = await _repo.AccountTypeExistsAsync(dto.AccountTypeId);
            if (!accountTypeExists)
            {
                throw new NotFoundException("Account Type by that id does not exist.");
            }

            var duplicateAccount = await _repo.ExistsByNameAsync(dto.AccountName);
            if (duplicateAccount)
            {
                throw new ConflictException("Account by this name already exists");
            }

            existingAccount.AccountName = dto.AccountName;
            existingAccount.AccountTypeId = dto.AccountTypeId;
            existingAccount.CurrentBalance = dto.CurrentBalance;

           
           await _repo.UpdateAsync(existingAccount);

            return true;
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var accountToDelete = await _repo.GetAccountByIdAsync(id);
            if (accountToDelete == null)
            {
                throw new NotFoundException("Account not found to delete");
            }

            await _repo.DeactivateAsync(accountToDelete);

            return true;
        }

       
    }
}
