
using PersonalFinanceTracker.Application.DTOs;
using PersonalFinanceTracker.Application.Exceptions;
using PersonalFinanceTracker.Application.Interfaces;
using PersonalFinanceTracker.Application.Services.Contracts;

namespace PersonalFinanceTracker.Application.Services
{
    public class AccountQueryService : IAccountQueryService
    {
        private readonly IAccountRepository _repo;

        public AccountQueryService(IAccountRepository repo)
        {
            _repo = repo;
        }

      

        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync(string? accountTypeName,bool? isActive, string? accountName)
        {
            var accountList = await _repo.GetAllAccountsAsync(accountTypeName, isActive, accountName);
            if (!accountList.Any())
            {
                throw new NotFoundException("No accounts avaialble.");

            }

            var accountDtos = new List<AccountDto>();

            foreach(var account in accountList)
            {
                var accountDto = new AccountDto
                {
                    Id = account.Id,
                    AccountName = account.AccountName,
                    AccountType = account.AccountType.Name,
                    CurrentBalance = account.CurrentBalance,
                    IsActive = account.IsActive
                };

                accountDtos.Add(accountDto);
            }

            return accountDtos; 
        }

       

        public async Task<AccountDto> GetAccountByIdAsync(int id)
        {
            var accountById = await _repo.GetAccountByIdAsync(id);
            if (accountById == null)
            {
                throw new NotFoundException("There is no account found by that id.");
            }

            var accountDto = new AccountDto
            {
                Id = accountById.Id,
                AccountName = accountById.AccountName,
                AccountType= accountById.AccountType.Name,
                CurrentBalance= accountById.CurrentBalance,
                IsActive = accountById.IsActive
            };

            return accountDto;
        }


    }
}
