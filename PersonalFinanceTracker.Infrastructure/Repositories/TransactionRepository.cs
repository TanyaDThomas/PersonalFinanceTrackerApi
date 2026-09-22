using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Application.Interfaces;
using PersonalFinanceTracker.Application.Models;
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Infrastructure.Persistence;
using static Azure.Core.HttpHeader;


namespace PersonalFinanceTracker.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly FinanceDbContext _context;

        public TransactionRepository(FinanceDbContext context)
        {
            _context = context;
        }

        // Command

        public async Task<bool> AccountExistsAsync(int accountId)
        {
            return await _context.Accounts.AnyAsync(a => a.Id == accountId);
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.Id == categoryId);
        }

        public async Task<bool> TransactionTypeExistsAsync(int transactionTypeId)
        {
            return await _context.TransactionTypes.AnyAsync(tt => tt.Id == transactionTypeId);
        }



        public async Task<bool> ExistsByNameAsync(string transactionDescription)
        {
            return await _context.Transactions.AnyAsync(t => t.Description == transactionDescription);
        }

        public async Task<bool> TransactionExistsAsync(int transactionId)
        {
            return await _context.Transactions.AnyAsync(t => t.Id == transactionId);
        }

        public async Task<bool> ExistsDuplicateAsync(decimal amount, DateTime transactionDate, int accountId, int id)
        {
            return await _context.Transactions.AnyAsync(t =>
                t.Amount == amount &&
                t.TransactionDate == transactionDate &&
                t.AccountId == accountId &&
                t.Id != id);
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<bool> UpdateAsync(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateAsync(Transaction transaction)
        {
            transaction.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }


        // Query

        public async Task<PagedResult<Transaction>> GetAllTransactionsAsync(TransactionQueryParameters parameters)
        {
            var query = _context.Transactions
                        .Include(t => t.Account)
                            .ThenInclude(a => a.AccountType)
                        .Include(t => t.Category)
                        .Include(t => t.TransactionType)
                        .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Description))
            {
                query = query.Where(t => t.Description.Contains(parameters.Description));
            }

            if (parameters.AccountId.HasValue)
            {
                query = query.Where(t => t.AccountId == parameters.AccountId.Value);
            }

            if (!string.IsNullOrWhiteSpace(parameters.CategoryName))
            {
                query = query.Where(t => t.Category.Name.Contains(parameters.CategoryName));
            }

            if (!string.IsNullOrWhiteSpace(parameters.TransactionTypeName))
            {
                query = query.Where(t => t.TransactionType.Name.Contains(parameters.TransactionTypeName));
            }

            if (parameters.MinAmount.HasValue)
            {
                query = query.Where(t => t.Amount >= parameters.MinAmount.Value);
            }

            if (parameters.MaxAmount.HasValue)
            {
                query = query.Where(t => t.Amount <= parameters.MaxAmount.Value);
            }

            if (parameters.StartDate.HasValue)
            {
                query = query.Where(t => t.TransactionDate >= parameters.StartDate.Value);
            }

            if (parameters.EndDate.HasValue)
            {
                query = query.Where(t => t.TransactionDate <= parameters.EndDate.Value);
            }

            var totalCount = await query.CountAsync();

            switch (parameters.SortBy?.ToLower())
            {
                case "transactiondate":
                    query = parameters.SortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(t => t.TransactionDate)
                        : query.OrderBy(t => t.TransactionDate);
                    break;

                case "amount":
                    query = parameters.SortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(t => t.Amount)
                        : query.OrderBy(t => t.Amount);
                    break;

                case "description":
                    query = parameters.SortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(t => t.Description)
                        : query.OrderBy(t => t.Description);
                    break;

                case "category":
                    query = parameters.SortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(t => t.Category.Name)
                        : query.OrderBy(t => t.Category.Name);
                    break;

                case "transactiontype":
                    query = parameters.SortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(t => t.TransactionType.Name)
                        : query.OrderBy(t => t.TransactionType.Name);
                    break;

                default:
                    query = query.OrderBy(t => t.Id);
                    break;
            }

            var transactions = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalCount / parameters.PageSize);

            return new PagedResult<Transaction>
            {
                Items = transactions,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions
                .Include(t => t.Account)
                    .ThenInclude(a => a.AccountType)
                .Include(t => t.Category)
                .Include(t => t.TransactionType)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

    }
}
