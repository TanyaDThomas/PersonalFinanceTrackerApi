
using PersonalFinanceTracker.Application.DTOs;
using PersonalFinanceTracker.Application.Exceptions;
using PersonalFinanceTracker.Application.Interfaces;
using PersonalFinanceTracker.Application.Services.Contracts;
using PersonalFinanceTracker.Domain.Entities;



namespace PersonalFinanceTracker.Application.Services
{
    public class TransactionCommandService : ITransactionCommandService
    {
            private readonly ITransactionRepository _repo;

            public TransactionCommandService(ITransactionRepository repo)
            {
                _repo = repo;
            }

            public async Task<Transaction> CreateAsync(CreateTransactionDto dto)
            {
                var exists = await _repo.ExistsByNameAsync(dto.Description);

                if (exists)
                {
                    throw new ConflictException("Transaction already exists");
                }

                var accountExists = await _repo.AccountExistsAsync(dto.AccountId);

                if (!accountExists)
                {
                    throw new NotFoundException("Account not found.");
                }

                var categoryExists = await _repo.CategoryExistsAsync(dto.CategoryId);

                if (!categoryExists)
                {
                    throw new NotFoundException("Category not found.");
                }

                var typeExists = await _repo.TransactionTypeExistsAsync(dto.TransactionTypeId);

                if (!typeExists)
                {
                    throw new NotFoundException("Transaction type not found.");
                }

                var transaction = new Transaction
                {
                    Description = dto.Description,
                    Amount = dto.Amount,
                    TransactionDate = dto.TransactionDate,
                    AccountId = dto.AccountId,
                    CategoryId = dto.CategoryId,
                    TransactionTypeId = dto.TransactionTypeId
                };

                await _repo.CreateAsync(transaction);

                return transaction;
            }

            public async Task<bool> UpdateAsync(int id, UpdateTransactionDto dto)
            {
                var existingTransaction = await _repo.GetTransactionByIdAsync(id);

                if (existingTransaction == null)
                {
                    throw new NotFoundException("Transaction not found.");
                }

                var duplicateExists = await _repo.ExistsDuplicateAsync(dto.Amount, dto.TransactionDate, dto.AccountId, id);

                if (duplicateExists)
                {
                    throw new ConflictException("Transaction already exists");
                }

                var accountExists = await _repo.AccountExistsAsync(dto.AccountId);

                if (!accountExists)
                {
                    throw new NotFoundException("Account not found.");
                }

                var categoryExists = await _repo.CategoryExistsAsync(dto.CategoryId);

                if (!categoryExists)
                {
                    throw new NotFoundException("Category not found.");
                }

                var transactionTypeExists = await _repo.TransactionTypeExistsAsync(dto.TransactionTypeId);

                if (!transactionTypeExists)
                {
                    throw new NotFoundException("Transaction type not found.");
                }

                existingTransaction.Description = dto.Description;
                existingTransaction.Amount = dto.Amount;
                existingTransaction.TransactionDate = dto.TransactionDate;
                existingTransaction.TransactionTypeId = dto.TransactionTypeId;
                existingTransaction.AccountId = dto.AccountId;
                existingTransaction.CategoryId = dto.CategoryId;

                await _repo.UpdateAsync(existingTransaction);

                return true;
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var transactionToDelete = await _repo.GetTransactionByIdAsync(id);

                if (transactionToDelete == null)
                {
                    throw new NotFoundException("Transaction not found.");
                }

                return await _repo.DeactivateAsync(transactionToDelete);
            }
        }
    }

