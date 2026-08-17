using PersonalFinanceTracker.Application.DTOs;
using PersonalFinanceTracker.Domain.Entities;

namespace PersonalFinanceTracker.Application.Services.Contracts
{
    public interface IAccountCommandService
    {
        Task<Account> CreateAsync(CreateAccountDto dto);
        Task<bool> UpdateAsync(int id, UpdateAccountDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
