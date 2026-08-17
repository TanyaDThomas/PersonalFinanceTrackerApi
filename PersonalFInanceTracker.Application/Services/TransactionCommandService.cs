using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Application.Services.Contracts;
using PersonalFinanceTracker.Application.DTOs;
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Application.Exceptions;
using PersonalFinanceTracker.Infrastructure.Persistence;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Application.Services
{
    public class TransactionCommandService : ITransactionCommandService
    {
        private readonly FinanceDbContext _context;

        public TransactionCommandService(FinanceDbContext context)
        {
            _context = context;
        }
        public async Task<Transaction> CreateAsync(CreateTransactionDto dto)
        {
            var exists = await _context.Transactions.AnyAsync(t => t.Amount == dto.Amount && t.TransactionDate == dto.TransactionDate && t.AccountId == dto.AccountId);
            if (exists)
            {
                throw new ConflictException("Transaction already exists");
            }

            var accountExists = await _context.Accounts.AnyAsync(a => a.Id == dto.AccountId);
            if(!accountExists)
            {
                throw new NotFoundException("Account not found.");
            }

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId);
            if(!categoryExists)
            {
                throw new NotFoundException("Category not found");
            }

            var typeExists = await _context.TransactionTypes.AnyAsync(tt => tt.Id == dto.TransactionTypeId);
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
                TransactionTypeId = dto.TransactionTypeId,
            };

            _context.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }


        public async Task<bool> UpdateAsync(int id, UpdateTransactionDto dto)
        {
            var existingTransaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
            if(existingTransaction == null)
            {
                throw new NotFoundException("Transaction Not Found");
            }

            var duplicateExists = await _context.Transactions.AnyAsync(t => t.Amount == dto.Amount && t.TransactionDate == dto.TransactionDate && t.AccountId == dto.AccountId && t.Id != id);
            if(duplicateExists)
            {
                throw new ConflictException("Transaction already exists");
            }

            var accountExists = await _context.Accounts.AnyAsync(a => a.Id == dto.AccountId);
            if(!accountExists)
            {
                throw new NotFoundException("Account not found.");
            }

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId);
            if(!categoryExists)
            {
                throw new NotFoundException("Category not found.");
               
            }

            var transactionTypeExists = await _context.TransactionTypes.AnyAsync(tt => tt.Id == dto.TransactionTypeId);
            if(!transactionTypeExists)
            {
                throw new NotFoundException("Transaction type not found");
            }

            existingTransaction.Description = dto.Description;
            existingTransaction.Amount = dto.Amount;
            existingTransaction.TransactionTypeId = dto.TransactionTypeId;
            existingTransaction.AccountId = dto.AccountId;
            existingTransaction.CategoryId = dto.CategoryId;
            existingTransaction.TransactionTypeId = dto.TransactionTypeId;

            await _context.SaveChangesAsync();

            return true;

        }

        
       

        public async Task<bool> DeleteAsync(int id)
        {
            var transactionToDelete = await _context.Transactions.FindAsync(id);
            if(transactionToDelete == null)
            {
                throw new NotFoundException("Transaction not found.");
            }

            _context.Remove(transactionToDelete);
            await _context.SaveChangesAsync();

            return true;
        }

   
    }
}
