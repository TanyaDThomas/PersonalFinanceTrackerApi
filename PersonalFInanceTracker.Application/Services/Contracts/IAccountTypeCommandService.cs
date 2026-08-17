
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Application.DTOs;

namespace PersonalFinanceTracker.Application.Services.Contracts
{
    public interface IAccountTypeCommandService
    {
        Task<AccountType> CreateAsync(CreateAccountTypeDto dto);
        Task<bool> UpdateAsync(int id, UpdateAccountTypeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
