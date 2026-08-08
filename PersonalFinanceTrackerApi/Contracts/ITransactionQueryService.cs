using PersonalFinanceTrackerApi.DTOs;
using PersonalFinanceTrackerApi.Models;
using System.Transactions;

namespace PersonalFinanceTrackerApi.Contracts
{
    public interface ITransactionQueryService
    {
        Task<PagedResult<TransactionDto>> GetAllAsync(TransactionQueryParameters parameters);
        Task<TransactionDto> GetByIdAsync(int id);
    }
}
