
using PersonalFinanceTracker.Application.DTOs;
using PersonalFinanceTracker.Domain.Entities;

namespace PersonalFinanceTracker.Application.Services.Contracts
{
    public interface ICategoryCommandService
    {
        Task<Category> CreateAsync(CreateCategoryDto dto);
        Task<bool> UpdateAsync(int id, UpdateCategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
