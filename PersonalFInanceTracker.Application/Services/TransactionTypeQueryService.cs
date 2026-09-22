
using PersonalFinanceTracker.Application.Services.Contracts;
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Application.Exceptions;
using PersonalFinanceTracker.Application.Interfaces;

namespace PersonalFinanceTracker.Application.Services
{
    public class TransactionTypeQueryService : ITransactionTypeQueryService
    {
        private readonly ITransactionTypeRepository _repo;

        public TransactionTypeQueryService(ITransactionTypeRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<TransactionType>> GetAll()
        {
            return await _repo.GetAllTransactionTypeAsync();
        }

        public async Task<TransactionType> GetById(int id)
        {
            var typeById = await _repo.GetTransactionTypeByIdAsync(id);


            if (typeById == null)
            {
                throw new NotFoundException("Transaction type not found.");
            }

            return typeById;
        }
    }
}
