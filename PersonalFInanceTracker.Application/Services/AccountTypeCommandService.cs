
using PersonalFinanceTracker.Application.Services.Contracts;
using PersonalFinanceTracker.Application.DTOs;
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Application.Exceptions;
using PersonalFinanceTracker.Application.Interfaces;


namespace PersonalFinanceTracker.Application.Services
{
    public class AccountTypeCommandService : IAccountTypeCommandService
    {
        private readonly IAccountTypeRepository _repo;

        public AccountTypeCommandService(IAccountTypeRepository repo)
        {
            _repo = repo;
        }
        public async Task<AccountType> CreateAsync(CreateAccountTypeDto dto)
        {
            var exists = await _repo.ExistsByNameAsync(dto.AccountTypeName);

            if (exists)
            {
                throw new ConflictException("There is already an account type by that name");
            }

            var accountType = new AccountType
            {
                Name = dto.AccountTypeName
            };

            await _repo.CreateAsync(accountType);

            return accountType;
           
        }

        public async Task<bool> UpdateAsync(int id, UpdateAccountTypeDto dto)
        {
            var existingType = await _repo.GetAccountTypeByIdAsync(id);
            if (existingType == null)
            {
                throw new NotFoundException("Account type not found.");
            }

            var duplicateExists = await _repo.ExistsByNameAsync(dto.AccountTypeName);
            if (duplicateExists)
            {
                throw new ConflictException("Account Type already exists");
            }

            existingType.Name = dto.AccountTypeName;

            await _repo.UpdateAsync(existingType);

            return true;
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var typeToDelete = await _repo.GetAccountTypeByIdAsync(id);
            if (typeToDelete == null)
            {
                throw new NotFoundException("Account type does not exist.");
            }

            _repo.DeactivateAsync(typeToDelete);

            return true;
            
        }

    }
}
