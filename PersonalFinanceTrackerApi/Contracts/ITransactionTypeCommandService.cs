using PersonalFinanceTrackerApi.DTOs;
using PersonalFinanceTrackerApi.Entities;

namespace PersonalFinanceTrackerApi.Contracts
{
    public interface ITransactionTypeCommandService
    {
        Task<TransactionType> CreateAsync(CreateTransactionTypeDto dto);
        Task<bool> UpdateAsync(int id, UpdateTransactionTypeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
