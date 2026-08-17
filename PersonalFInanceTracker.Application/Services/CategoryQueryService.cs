//using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Application.Services.Contracts;
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Application.Exceptions;
using PersonalFinanceTracker.Infrastructure.Persistence;

namespace PersonalFinanceTracker.Application.Services
{
    public class CategoryQueryService : ICategoryQueryService
    {
        private readonly FinanceDbContext _context;

        public CategoryQueryService(FinanceDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .ToListAsync();  
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var categoryById = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (categoryById == null)
            {
                throw new NotFoundException("Category could not be found");
            }

            return categoryById;
        }
    }
}
