using Microsoft.EntityFrameworkCore;
using PersonalFinanceTrackerApi.Contracts;
using PersonalFinanceTrackerApi.DTOs;
using PersonalFinanceTrackerApi.Exceptions;
using PersonalFinanceTrackerApi.Persistence;
using System.Transactions;

namespace PersonalFinanceTrackerApi.Services
{
    public class TransactionQueryService : ITransactionQueryService
    {
        private readonly FinanceDbContext _context;

        public TransactionQueryService(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllAsync(TransactionQueryParameters parameters)
        {
            var query = _context.Transactions.AsQueryable();

            if(!string.IsNullOrWhiteSpace(parameters.Description))
            {
                query = query.Where(t => t.Description.Contains(parameters.Description));
;            }

            if(parameters.AccountId.HasValue)
            {
                query = query.Where(t => t.AccountId == parameters.AccountId.Value);
            }

            if(!string.IsNullOrWhiteSpace(parameters.CategoryName))
            {
                query = query.Where(t => t.Category.Name.Contains(parameters.CategoryName));
            }

            if(!string.IsNullOrWhiteSpace(parameters.TransactionTypeName))
            {
                query = query.Where(t => t.TransactionType.Name.Contains(parameters.TransactionTypeName));
            }

            if(parameters.MinAmount.HasValue)
            {
                query = query.Where(t => t.Amount >=  parameters.MinAmount.Value);
            }

            if(parameters.MaxAmount.HasValue)
            {
                query = query.Where(t => t.Amount <=  parameters.MaxAmount.Value);
            }

            if(parameters.StartDate.HasValue)
            {
                query = query.Where(t => t.TransactionDate >= parameters.StartDate.Value);
            }
                
            if(parameters.EndDate.HasValue)
            {
                query = query.Where(t => t.TransactionDate <= parameters.EndDate.Value);
            }

            return await query
            //return await _context.Transactions
                .AsNoTracking()
                .AsSplitQuery()
                .Include(a => a.Account)
                    .ThenInclude(at => at.AccountType)
                .Include(c => c.Category)
                .Include(tt => tt.TransactionType)
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    Amount = t.Amount,
                    TransactionDate = t.TransactionDate,
                    Account = t.Account.AccountName,
                    AccountType = t.Account.AccountType.Name,
                    Category = t.Category.Name,
                    TransactionType = t.TransactionType.Name,

                })
                .ToListAsync();
        }

        public async Task<TransactionDto> GetByIdAsync(int id)
        {
            var transactionById = await _context.Transactions
                .AsSplitQuery()
                .Include(a => a.Account)
                    .ThenInclude(at => at.AccountType)
                .Include(c => c.Category)
                .Include(tt => tt.TransactionType)
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    Amount = t.Amount,
                    TransactionDate = t.TransactionDate,
                    Account = t.Account.AccountName,
                    AccountType = t.Account.AccountType.Name,
                    Category = t.Category.Name,
                    TransactionType = t.TransactionType.Name,
                })
                .FirstOrDefaultAsync(t => t.Id == id);

            if(transactionById == null)
            {
                throw new NotFoundException("Transaction not found");
            }

            return transactionById;
        }

     
    }
}
