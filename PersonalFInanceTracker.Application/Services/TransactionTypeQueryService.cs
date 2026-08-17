using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Application.Services.Contracts;
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Application.Exceptions;
using PersonalFinanceTracker.Infrastructure.Persistence;

namespace PersonalFinanceTracker.Application.Services
{
    public class TransactionTypeQueryService : ITransactionTypeQueryService
    {
        private readonly FinanceDbContext _context;

        public TransactionTypeQueryService(FinanceDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TransactionType>> GetAll()
        {
            return await _context.TransactionTypes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TransactionType> GetById(int id)
        {
            var typeById = await _context.TransactionTypes.FirstOrDefaultAsync(tt => tt.Id == id);
            
            if(typeById == null)
            {
                throw new NotFoundException("Transaction type not found.");
            }

            return typeById;
        }
    }
}
