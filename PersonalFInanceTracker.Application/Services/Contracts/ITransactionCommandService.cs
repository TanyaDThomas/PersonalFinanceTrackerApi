
using PersonalFinanceTracker.Application.DTOs;
using PersonalFinanceTracker.Domain.Entities;

namespace PersonalFinanceTracker.Application.Services.Contracts
{
    public interface ITransactionCommandService
    {
        Task<Transaction> CreateAsync(CreateTransactionDto dto);
        Task<bool> UpdateAsync(int id, UpdateTransactionDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
