
using PersonalFinanceTracker.Application.DTOs;
using PersonalFinanceTracker.Domain.Entities;

namespace PersonalFinanceTracker.Application.Services.Contracts
{
    public interface ITransactionTypeCommandService
    {
        Task<TransactionType> CreateAsync(CreateTransactionTypeDto dto);
        Task<bool> UpdateAsync(int id, UpdateTransactionTypeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
