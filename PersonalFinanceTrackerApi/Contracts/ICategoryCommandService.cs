using PersonalFinanceTrackerApi.DTOs;
using PersonalFinanceTrackerApi.Entities;

namespace PersonalFinanceTrackerApi.Contracts
{
    public interface ICategoryCommandService
    {
        Task<Category> CreateAsync(CreateCategoryDto dto);
        Task<bool> UpdateAsync(int id, UpdateCategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
