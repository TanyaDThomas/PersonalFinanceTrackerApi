
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Application.DTOs;

namespace PersonalFinanceTracker.Application.Services.Contracts
{
    public interface IAccountQueryService
    {
        Task<IEnumerable<AccountDto>> GetAllAccountsAsync(string? accountTypeName, bool? isActive, string? accountName);
        Task<AccountDto> GetAccountsByIdAsync(int id);
    }
}
