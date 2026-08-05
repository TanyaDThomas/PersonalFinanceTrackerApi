using PersonalFinanceTrackerApi.Entities;

namespace PersonalFinanceTrackerApi.Contracts
{
    public interface ITransactionTypeQueryService
    {
        Task<IEnumerable<TransactionType>> GetAll();
        Task<TransactionType> GetById(int id);
    }
}
