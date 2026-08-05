using Microsoft.EntityFrameworkCore;
using PersonalFinanceTrackerApi.Contracts;
using PersonalFinanceTrackerApi.DTOs;
using PersonalFinanceTrackerApi.Entities;
using PersonalFinanceTrackerApi.Exceptions;
using PersonalFinanceTrackerApi.Persistence;

namespace PersonalFinanceTrackerApi.Services
{
    public class TransactionTypeCommandService : ITransactionTypeCommandService
    {
        private readonly FinanceDbContext _context;

        public TransactionTypeCommandService(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionType> CreateAsync(CreateTransactionTypeDto dto)
        {
            var exists = await _context.TransactionTypes.AnyAsync(tt => tt.Name == dto.Name);

            if(exists)
            {
                throw new ConflictException("Transaction type already exists!");
            }

            var transactionType = new TransactionType
            {
                Name = dto.Name
            };

            _context.Add(transactionType);
            await _context.SaveChangesAsync();

            return transactionType;

        }

        public async Task<bool> UpdateAsync(int id, UpdateTransactionTypeDto dto)
        {
            var existingType = await _context.TransactionTypes.FindAsync(id);
            if(existingType == null)
            {
                throw new NotFoundException("Transaction type not found.");
            }

            var duplicateExists = await _context.TransactionTypes.AnyAsync(tt => tt.Name == dto.Name && tt.Id != id);
            if(duplicateExists)
            {
                throw new ConflictException("Transaction type already exists.");
              
            }

            existingType.Name = dto.Name;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var typeToDelete = await _context.TransactionTypes.FindAsync(id);
            if(typeToDelete == null)
            {
                throw new NotFoundException("Transaction type not found.");
            }

            _context.Remove(typeToDelete);
            await _context.SaveChangesAsync();

            return true;


        }

    }
}
