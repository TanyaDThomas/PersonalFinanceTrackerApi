using Microsoft.EntityFrameworkCore;
using Octopus.Client.Model.Accounts;
using PersonalFinanceTracker.Application.Interfaces;
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Infrastructure.Repositories
{
    public class TransactionTypeRepository : ITransactionTypeRepository
    {
        private readonly FinanceDbContext _context;

        public TransactionTypeRepository(FinanceDbContext context)
        {
            _context = context;
        }

        // Command
        public async Task<bool> ExistsByNameAsync(string transactionTypeName)
        {
            return await _context.TransactionTypes.AnyAsync(tt => tt.Name == transactionTypeName);
        }

        public async Task<bool> TransactionTypeExistsAsync(int transactionTypeId)
        {
            return await _context.TransactionTypes.AnyAsync(tt => tt.Id == transactionTypeId);
        }

        public async Task<TransactionType> CreateAsync(TransactionType transactionType)
        {
            _context.TransactionTypes.Add(transactionType);
            await _context.SaveChangesAsync();
            return transactionType;
        }

        public async Task<bool> UpdateAsync(TransactionType transactionType)
        {
            _context.TransactionTypes.Update(transactionType);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateAsync(TransactionType transactionType)
        {
            transactionType.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

      
        // Query 
        public async Task<IEnumerable<TransactionType>> GetAllTransactionTypeAsync()
        {
            return await _context.TransactionTypes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TransactionType?> GetTransactionTypeByIdAsync(int id)
        {
            return await _context.TransactionTypes.FindAsync(id);
        }

    

       
    }
}
