using PersonalFinanceTracker.Application.Models;
using PersonalFinanceTracker.Domain.Entities;


namespace PersonalFinanceTracker.Application.Interfaces
{
   public interface ITransactionRepository
    {
        // Query
        Task<PagedResult<Transaction>> GetAllTransactionsAsync(TransactionQueryParameters parameters);
        Task<Transaction?> GetTransactionByIdAsync(int id);

        // Command

        Task<bool> AccountExistsAsync(int accountId);
        Task<bool> CategoryExistsAsync(int categoryId);
        Task<bool> TransactionTypeExistsAsync(int transactionTypeId);
        Task<bool> ExistsByNameAsync(string transactionDescription);
        Task<bool> TransactionExistsAsync(int transactionId);
        Task<bool> ExistsDuplicateAsync(decimal amount, DateTime transactionDate, int accountId, int id);
        Task<Transaction> CreateAsync(Transaction transaction);


        Task<bool> UpdateAsync(Transaction transaction);
        Task<bool> DeactivateAsync(Transaction transaction);
    }
}
