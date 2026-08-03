using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTrackerApi.Entities;

namespace PersonalFinanceTrackerApi.Contracts
{
   
    public interface ICategoryQueryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
    }
}
