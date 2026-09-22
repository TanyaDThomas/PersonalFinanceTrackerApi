using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Application.Interfaces;
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Infrastructure.Persistence;


namespace PersonalFinanceTracker.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FinanceDbContext _context;
        public CategoryRepository(FinanceDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }


        // Command
        public async Task<bool> ExistsByNameAsync(string categoryName)
        {
            return await _context.Categories.AnyAsync(c => c.Name == categoryName);
        }
        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.Id == categoryId);
        }
        public async Task<Category> CreateAsync(Category category)
        {
            
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeactivateAsync(Category category)
        {
            category.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
