

using PersonalFinanceTracker.Application.DTOs;
using PersonalFinanceTracker.Domain.Entities;

namespace PersonalFinanceTracker.Tests
{
    public class AccountTestData
    {
        public static Account CreateAccount()
        {
            return new Account
            {
                Id = 1,
                AccountName = "Mary Dallas",
                AccountTypeId = 1,
                AccountType = new AccountType
                {
                    Id = 1,
                    Name = "Checking"
                },
                CurrentBalance = 6634.54M,
                IsActive = true
            };
        }

        public static CreateAccountDto CreateAccountDto()
        {
            return new CreateAccountDto
            {
                AccountName = "Maria Dalis",
                AccountTypeId = 1,
                CurrentBalance = 3234.54M
            };
        }

        public static UpdateAccountDto UpdateAccountDto()
        {
            return new UpdateAccountDto
            {
                AccountName = "Maria Dalis",
                AccountTypeId = 1,
                CurrentBalance = 3234.54M
            };
        }
    }
}
