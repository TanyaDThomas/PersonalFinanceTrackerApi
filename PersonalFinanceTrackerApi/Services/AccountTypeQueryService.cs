using Microsoft.EntityFrameworkCore;
using PersonalFinanceTrackerApi.Contracts;
using PersonalFinanceTrackerApi.Entities;
using PersonalFinanceTrackerApi.Exceptions;
using PersonalFinanceTrackerApi.Persistence;

namespace PersonalFinanceTrackerApi.Services
{
    public class AccountTypeQueryService : IAccountTypeQueryService
    {
        private readonly FinanceDbContext _context;

        public AccountTypeQueryService(FinanceDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AccountType>> GetAllAsync()
        {
            return await _context.AccountTypes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AccountType> GetTypeByIdAsync(int id)
        {
            var typeById = await _context.AccountTypes.FirstOrDefaultAsync(at => at.Id == id);

            if (typeById == null)
            {
                throw new NotFoundException("Account Type not found");
            }

            return typeById;
        }

    }
}
