
using PersonalFinanceTracker.Application.DTOs;
using PersonalFinanceTracker.Application.Models;
using System.Transactions;

namespace PersonalFinanceTracker.Application.Services.Contracts
{
    public interface ITransactionQueryService
    {
        Task<PagedResult<TransactionDto>> GetAllAsync(TransactionQueryParameters parameters);
        Task<TransactionDto> GetByIdAsync(int id);
    }
}
