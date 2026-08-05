using PersonalFinanceTrackerApi.DTOs;
using System.Transactions;

namespace PersonalFinanceTrackerApi.Contracts
{
    public interface ITransactionQueryService
    {
        Task<IEnumerable<TransactionDto>> GetAllAsync();
        Task<TransactionDto> GetByIdAsync(int id);
    }
}
