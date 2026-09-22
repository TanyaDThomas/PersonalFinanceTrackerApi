using PersonalFinanceTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceTracker.Application.Interfaces
{
    public interface ITransactionTypeRepository
    {
        // Query
        Task<IEnumerable<TransactionType>> GetAllTransactionTypeAsync();
        Task<TransactionType?> GetTransactionTypeByIdAsync(int id);

        // Command
        Task<bool> ExistsByNameAsync(string transactionTypeName);
        Task<bool> TransactionTypeExistsAsync(int transactionTypeId);
        Task<TransactionType> CreateAsync(TransactionType transactionType);


        Task<bool> UpdateAsync(TransactionType transactionType);
        Task<bool> DeactivateAsync(TransactionType transactionType);
    }
}
