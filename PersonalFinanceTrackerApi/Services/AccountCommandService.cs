using Microsoft.Identity.Client;
using PersonalFinanceTrackerApi.Contracts;
using PersonalFinanceTrackerApi.DTOs;
using PersonalFinanceTrackerApi.Entities;
using PersonalFinanceTrackerApi.Persistence;

namespace PersonalFinanceTrackerApi.Services
{
    public class AccountCommandService : IAccountCommandService
    {
        private readonly FinanceDbContext _context;

        public AccountCommandService(FinanceDbContext context)
        {
            _context = context;
        }
        public Task<Account> CreateAsync(CreateAccountDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, UpdateAccountDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

      
    }
}
