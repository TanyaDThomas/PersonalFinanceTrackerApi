using PersonalFinanceTrackerApi.DTOs;
using System.Transactions;

namespace PersonalFinanceTrackerApi.Contracts
{
    public interface ITransactionQueryService
    {
        Task<IEnumerable<TransactionDto>> GetAllAsync(TransactionQueryParameters parameters);
        Task<TransactionDto> GetByIdAsync(int id);
    }
}
