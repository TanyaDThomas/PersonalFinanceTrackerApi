using Moq;
using PersonalFinanceTracker.Application.Exceptions;
using PersonalFinanceTracker.Application.Interfaces;
using PersonalFinanceTracker.Application.Services;
using PersonalFinanceTracker.Domain.Entities;


namespace PersonalFinanceTracker.Tests
{
    public class AccountQueryServiceTests
    {
        private readonly Mock<IAccountRepository> _repoMock;
        private readonly AccountQueryService _queryService;
        public AccountQueryServiceTests()
        {
            _repoMock = new Mock<IAccountRepository>();
            _queryService = new AccountQueryService(_repoMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WhenAccountExists_ReturnsAccountDto()
        {
            // Arrange
            var account = new Account
            {
                Id = 1,
                AccountName = "Account Test",
                AccountType = new AccountType
                {
                    Id = 1,
                    Name = "Checking"
                },
                CurrentBalance = 38413.42M,
                IsActive = true

            };

            _repoMock.Setup(repo => repo.GetAccountByIdAsync(account.Id))
                .ReturnsAsync(account);

            // Act
            var result = await _queryService.GetAccountByIdAsync(account.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Account Test", result.AccountName);
        }


        [Fact]
        public async Task GetByidAsync_WhenAccountDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var accountId = 999;

            _repoMock.Setup(repo => repo.GetAccountByIdAsync(accountId)).ReturnsAsync((Account?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(
                () => _queryService.GetAccountByIdAsync(accountId));
        }

        [Fact]
        public async Task GetAllAccountsAsync_WhenAccountsExist_ReturnsAccountDtos()
        {
            // Arrange
            var allAccounts = new List<Account>
            {
                new Account
                {
                    Id = 1,
                    AccountName = "Cherl Miley",
                    AccountType = new AccountType { Id = 1, Name = "Checking" },
                    IsActive = true
                },
                new Account
                {
                    Id = 2,
                    AccountName = "Terryl Davis",
                    AccountType = new AccountType { Id = 2, Name = "Savings" },
                    IsActive = true
                }
            };

            _repoMock.Setup(repo => repo.GetAllAccountsAsync(null, null, null)).ReturnsAsync(allAccounts);

            // Act
            var result = (await _queryService.GetAllAccountsAsync(null, null, null)).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Cherl Miley", result[0].AccountName);
            Assert.Equal("Terryl Davis", result[1].AccountName);
        }

        [Fact]
        public async Task GetAllAccountsAsync_WhenNoAccountsExist_ThrowsNotFoundException()
        {
            // Arrange
            _repoMock
                .Setup(repo => repo.GetAllAccountsAsync(null, null, null))
                .ReturnsAsync(new List<Account>());

           
            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(
                () => _queryService.GetAllAccountsAsync(null, null, null));
        }
    }
}
