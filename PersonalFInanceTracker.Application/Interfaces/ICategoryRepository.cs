using PersonalFinanceTracker.Domain.Entities;


namespace PersonalFinanceTracker.Application.Interfaces
{
    public interface ICategoryRepository
    {
        // Query
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);


        // Command
        Task<bool> ExistsByNameAsync(string categoryName);
        Task<bool> CategoryExistsAsync(int accountTypeId);
        Task<Category> CreateAsync(Category category);

        Task<bool> UpdateAsync(Category category);
        Task<bool> DeactivateAsync(Category category);
    }
}
