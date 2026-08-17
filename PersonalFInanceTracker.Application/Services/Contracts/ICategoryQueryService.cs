//using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.Domain.Entities;

namespace PersonalFinanceTracker.Application.Services.Contracts
{
   
    public interface ICategoryQueryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
    }
}
