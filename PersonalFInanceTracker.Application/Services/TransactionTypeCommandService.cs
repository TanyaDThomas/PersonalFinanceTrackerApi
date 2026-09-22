
using PersonalFinanceTracker.Application.DTOs;
using PersonalFinanceTracker.Application.Exceptions;
using PersonalFinanceTracker.Application.Interfaces;
using PersonalFinanceTracker.Application.Services.Contracts;
using PersonalFinanceTracker.Domain.Entities;


namespace PersonalFinanceTracker.Application.Services
{
    public class TransactionTypeCommandService : ITransactionTypeCommandService
    {
        private readonly ITransactionTypeRepository _repo;

        public TransactionTypeCommandService(ITransactionTypeRepository repo)
        {
            _repo = repo;
        }

        public async Task<TransactionType> CreateAsync(CreateTransactionTypeDto dto)
        {
            var exists = await _repo.ExistsByNameAsync(dto.Name);

            if (exists)
            {
                throw new ConflictException("There is already a transaction type by that name");
            }

            var transactionType = new TransactionType
            {
                Name = dto.Name
            };

            await _repo.CreateAsync(transactionType);

            return transactionType;
        }

        public async Task<bool> UpdateAsync(int id, UpdateTransactionTypeDto dto)
        {
            var existingTransactionType = await _repo.GetTransactionTypeByIdAsync(id);

            if (existingTransactionType == null)
            {
                throw new NotFoundException("Transaction type not found");
            }

            var duplicateExists = await _repo.ExistsByNameAsync(dto.Name);

            if (duplicateExists)
            {
                throw new ConflictException("Transaction type by this name already exists");
            }

            existingTransactionType.Name = dto.Name;

            await _repo.UpdateAsync(existingTransactionType);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transactionTypeToDelete = await _repo.GetTransactionTypeByIdAsync(id);
            if (transactionTypeToDelete == null)
            {
                throw new NotFoundException("Account not found to delete");
            }

            await _repo.DeactivateAsync(transactionTypeToDelete);

            return true;
        }

    }
}
