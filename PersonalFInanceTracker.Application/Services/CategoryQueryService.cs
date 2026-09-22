
using PersonalFinanceTracker.Application.Services.Contracts;
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Application.Exceptions;
using PersonalFinanceTracker.Application.Interfaces;


namespace PersonalFinanceTracker.Application.Services
{
    public class CategoryQueryService : ICategoryQueryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryQueryService(ICategoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _repo.GetAllCategoriesAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var categoryById = await _repo.GetCategoryByIdAsync(id);
            if (categoryById == null)
            {
                throw new NotFoundException("Category could not be found");
            }

            var category = new Category
            {
                Id = categoryById.Id,
                Name = categoryById.Name,
            };

            return category;
        }
    }
}
