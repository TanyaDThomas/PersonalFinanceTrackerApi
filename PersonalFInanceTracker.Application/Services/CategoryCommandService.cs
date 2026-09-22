
using PersonalFinanceTracker.Application.Services.Contracts;
using PersonalFinanceTracker.Application.DTOs;
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Application.Exceptions;
using PersonalFinanceTracker.Application.Interfaces;

namespace PersonalFinanceTracker.Application.Services
{
    public class CategoryCommandService : ICategoryCommandService
    {
        private readonly ICategoryRepository _repo;

        public CategoryCommandService(ICategoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<Category> CreateAsync(CreateCategoryDto dto)
        {
            var exists = await _repo.ExistsByNameAsync(dto.Name);
            if (exists)
            {
                throw new ConflictException("Category already exists");
            }

            var category = new Category
            {
                Name = dto.Name
            };

            await _repo.CreateAsync(category);

            return category;
            
        }

        public async Task<bool> UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var existingCategory = await _repo.GetCategoryByIdAsync(id);
            _repo.ExistsByNameAsync(dto.Name);

            if(existingCategory == null)
            {
                throw new NotFoundException("That category does not exist.");
            }

            var duplicateExists = await _repo.ExistsByNameAsync(dto.Name);
            if (duplicateExists)
            {
                throw new ConflictException("There is already a category by this name.");
            }

            existingCategory.Name = dto.Name;

            await _repo.UpdateAsync(existingCategory);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categoryToDelete = await _repo.GetCategoryByIdAsync(id);
            if (categoryToDelete == null)
            {
                throw new NotFoundException("Could not find category to delete.");
            }

            await _repo.DeactivateAsync(categoryToDelete);

            return true;
        }

    }
}
