
using PersonalFinanceTracker.Application.Services.Contracts;
using PersonalFinanceTracker.Application.DTOs;
using PersonalFinanceTracker.Application.Exceptions;
using PersonalFinanceTracker.Application.Models;
using System.Runtime.InteropServices;
using PersonalFinanceTracker.Application.Interfaces;


namespace PersonalFinanceTracker.Application.Services
{
    public class TransactionQueryService : ITransactionQueryService
    {
        private readonly ITransactionRepository _repo;

        public TransactionQueryService(ITransactionRepository repo)
        {
            _repo = repo;
        }

        public async Task<PagedResult<TransactionDto>> GetAllAsync(TransactionQueryParameters parameters)
        {
            var result = await _repo.GetAllTransactionsAsync(parameters);

            var transactions = result.Items.Select(t => new TransactionDto
            {
                Id = t.Id,
                Description = t.Description,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate,
                Account = t.Account.AccountName,
                AccountType = t.Account.AccountType.Name,
                Category = t.Category.Name,
                TransactionType = t.TransactionType.Name
            }).ToList();

            return new PagedResult<TransactionDto>
            {
                Items = transactions,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
                TotalPages = result.TotalPages
            };
        }


        public async Task<TransactionDto> GetByIdAsync(int id)
        {
            var transaction = await _repo.GetTransactionByIdAsync(id);

            if (transaction == null)
            {
                throw new NotFoundException("Transaction not found");
            }

            return new TransactionDto
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate,
                Account = transaction.Account.AccountName,
                AccountType = transaction.Account.AccountType.Name,
                Category = transaction.Category.Name,
                TransactionType = transaction.TransactionType.Name
            };
        }

    }
}
