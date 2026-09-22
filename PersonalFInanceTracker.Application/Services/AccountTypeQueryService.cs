
using PersonalFinanceTracker.Application.Exceptions;
using PersonalFinanceTracker.Application.Interfaces;
using PersonalFinanceTracker.Application.Services.Contracts;
using PersonalFinanceTracker.Domain.Entities;



namespace PersonalFinanceTracker.Application.Services
{
    public class AccountTypeQueryService : IAccountTypeQueryService
    {
        private readonly IAccountTypeRepository _repo;

        public AccountTypeQueryService(IAccountTypeRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<AccountType>> GetAllAsync()
        {
            return await _repo.GetAllAccountTypesAsync();
        }

        public async Task<AccountType> GetTypeByIdAsync(int id)
        {
            var typeById = await _repo.GetAccountTypeByIdAsync(id);

            if (typeById == null)
            {
                throw new NotFoundException("Account Type not found");
            }

            return typeById;
        }

    }
}
