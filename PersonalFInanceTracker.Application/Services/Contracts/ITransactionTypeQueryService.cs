using PersonalFinanceTracker.Domain.Entities;

namespace PersonalFinanceTracker.Application.Services.Contracts
{
    public interface ITransactionTypeQueryService
    {
        Task<IEnumerable<TransactionType>> GetAll();
        Task<TransactionType> GetById(int id);
    }
}
