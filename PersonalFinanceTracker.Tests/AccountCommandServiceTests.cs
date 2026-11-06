

using Moq;
using PersonalFinanceTracker.Application.Exceptions;
using PersonalFinanceTracker.Application.Interfaces;
using PersonalFinanceTracker.Application.Services;
using PersonalFinanceTracker.Domain.Entities;


namespace PersonalFinanceTracker.Tests
{
    public class AccountCommandServiceTests
    {
        private readonly Mock<IAccountRepository> _repoMock;
        private readonly AccountCommandService _commandService;

        public AccountCommandServiceTests()
        {
            _repoMock = new Mock<IAccountRepository>();
            _commandService = new AccountCommandService(_repoMock.Object);
        }

        // CREATE

        [Fact]
        public async Task CreateAsync_WhenAccountIsValid_CreatesAndReturnsAccount()
        {
            // Arrange

            var dto = AccountTestData.CreateAccountDto();

            _repoMock.Setup(repo => repo.AccountTypeExistsAsync(dto.AccountTypeId))
                .ReturnsAsync(true);

            _repoMock.Setup(repo => repo.ExistsByNameAsync(dto.AccountName))
                .ReturnsAsync(false);

            _repoMock.Setup(repo => repo.CreateAsync(It.IsAny<Account>())).ReturnsAsync((Account account) => account);

            //Act
            var result = await _commandService.CreateAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.AccountName, result.AccountName);
            Assert.Equal(dto.AccountTypeId, result.AccountTypeId);
            Assert.Equal(dto.CurrentBalance, result.CurrentBalance);

            _repoMock.Verify(repo => repo.CreateAsync(It.IsAny<Account>()),
              Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WhenAccountAlreadyExists_ThrowsConflictException()
        {
            // Arrange

            var dto = AccountTestData.CreateAccountDto();

            _repoMock
               .Setup(repo => repo.ExistsByNameAsync(dto.AccountName))
               .ReturnsAsync(true);

            // Act & Arrange
            await Assert.ThrowsAsync<ConflictException>(
                () => _commandService.CreateAsync(dto));

            _repoMock.Verify(repo => repo.CreateAsync(It.IsAny<Account>()),
                Times.Never);
        }

        // UPDATE

        [Fact]
        public async Task UpdateAsync_WhenAccountDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var accountId = 1;

            var dto = AccountTestData.UpdateAccountDto();

            _repoMock
              .Setup(repo => repo.GetAccountByIdAsync(accountId))
              .ReturnsAsync((Account?)null);


            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(
                () => _commandService.UpdateAsync(accountId, dto));

            _repoMock.Verify(repo => repo.UpdateAsync(It.IsAny<Account>()),
                Times.Never);

        }

        [Fact]
        public async Task UpdateAsync_WhenAccountAlreadyExists_ThrowsConflictException()
        {
            var accountId = 1;

            var existingAccount = AccountTestData.CreateAccount();

            var dto = AccountTestData.UpdateAccountDto();


            _repoMock.Setup(repo => repo.GetAccountByIdAsync(accountId))
                .ReturnsAsync(existingAccount);

            _repoMock.Setup(repo => repo.ExistsByNameAsync(dto.AccountName))
                .ReturnsAsync(false);

            _repoMock
            .Setup(repo => repo.AccountTypeExistsAsync(dto.AccountTypeId))
            .ReturnsAsync(true);

            // Act
            var result = await _commandService.UpdateAsync(accountId, dto);

            // Assert
            Assert.True(result);
            Assert.Equal(dto.AccountName, existingAccount.AccountName);
            Assert.Equal(dto.AccountTypeId, existingAccount.AccountTypeId);
            Assert.Equal(dto.CurrentBalance, existingAccount.CurrentBalance);

            _repoMock.Verify(repo => repo.UpdateAsync(existingAccount),
                Times.Once);
        }

        // DEACTIVATE
        [Fact]
        public async Task DeactivateAsync_WhenAccountDoesNotExist_ThrowNotFoundError()
        { 
            // Arrange
            var accountId = 1;

            _repoMock.Setup(repo => repo.GetAccountByIdAsync(accountId))
                .ReturnsAsync((Account?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(
                () => _commandService.DeactivateAsync(accountId));
        }

        [Fact]
        public async Task DeactivateAsync_WhenAccountExists_DeactivateAndReturnSuccess()
        {
            // Arrange
            var accountId = 1;

            var existingAccount = AccountTestData.CreateAccount();

            _repoMock.Setup(repo => repo.GetAccountByIdAsync(accountId))
                .ReturnsAsync(existingAccount);

            // Act
            var result = await _commandService.DeactivateAsync(accountId);

            // Assert
            Assert.True(result);

            _repoMock.Verify(repo => repo.DeactivateAsync(existingAccount),
                Times.Once);
        }
    }
}
