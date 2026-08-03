using Microsoft.EntityFrameworkCore;
using PersonalFinanceTrackerApi.Contracts;
using PersonalFinanceTrackerApi.DTOs;
using PersonalFinanceTrackerApi.Entities;
using PersonalFinanceTrackerApi.Exceptions;
using PersonalFinanceTrackerApi.Persistence;

namespace PersonalFinanceTrackerApi.Services
{
    public class CategoryCommandService : ICategoryCommandService
    {
        private readonly FinanceDbContext _context;

        public CategoryCommandService(FinanceDbContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateAsync(CreateCategoryDto dto)
        {
            var exists = await _context.Categories.AnyAsync(c => c.Name == dto.Name);
            if(exists)
            {
                throw new ConflictException("Category already exists");
            }

            var category = new Category
            {
                Name = dto.Name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
            
        }

        public async Task<bool> UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var existingCategory = await _context.Categories.FindAsync(id);

            if(existingCategory == null)
            {
                throw new NotFoundException("That category does not exist.");
            }

            var duplicateExists = await _context.Categories.AnyAsync(c => c.Name == dto.Name && c.Id != id);
            if(duplicateExists)
            {
                throw new ConflictException("There is already a category by this name.");
            }

            existingCategory.Name = dto.Name;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categoryToDelete = await _context.Categories.FindAsync(id);
            if(categoryToDelete == null)
            {
                throw new NotFoundException("Could not find category to delete.");
            }

            _context.Remove(categoryToDelete);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
