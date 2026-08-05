using PersonalFinanceTrackerApi.DTOs;
using PersonalFinanceTrackerApi.Entities;

namespace PersonalFinanceTrackerApi.Contracts
{
    public interface ITransactionCommandService
    {
        Task<Transaction> CreateAsync(CreateTransactionDto dto);
        Task<bool> UpdateAsync(int id, UpdateTransactionDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
