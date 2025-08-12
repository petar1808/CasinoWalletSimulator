using CasinoWallet.Services;
using FluentAssertions;

namespace CasinoWallet.Tests.Tests.Services
{
    public class WalletServiceTests
    {
        [Fact]
        public void Deposit_ShouldIncreaseBalance_WhenAmountIsPositive()
        {
            // Arrange
            var service = new WalletService();
            var amount = 100;

            // Act
            var result = service.Deposit(amount);

            // Assert
            result.IsSuccess.Should().BeTrue();
            service.Balance.Should().Be(amount);
        }

        [Fact]
        public void Deposit_ShouldFail_WhenAmountIsZeroOrNegative()
        {
            // Arrange
            var service = new WalletService();
            var amount = 0;

            // Act
            var result = service.Deposit(amount);

            // Assert
            result.IsSuccess.Should().BeFalse();
            service.Balance.Should().Be(0);
        }

        [Fact]
        public void Withdraw_ShouldDecreaseBalance_WhenEnoughFunds()
        {
            // Arrange
            var service = new WalletService();
            service.Deposit(200);
            var amount = 50;

            // Act
            var result = service.Withdraw(amount);

            // Assert
            result.IsSuccess.Should().BeTrue();
            service.Balance.Should().Be(150);
        }

        [Fact]
        public void Withdraw_ShouldFail_WhenInsufficientFunds()
        {
            // Arrange
            var service = new WalletService();
            service.Deposit(50);
            var amount = 100;

            // Act
            var result = service.Withdraw(amount);

            // Assert
            result.IsSuccess.Should().BeFalse();
            service.Balance.Should().Be(50);
        }

        [Fact]
        public void UpdateBalance_ShouldIncreaseBalance_WhenAmountIsPositive()
        {
            // Arrange
            var service = new WalletService();
            var amount = 30;

            // Act
            var result = service.UpdateBalance(amount);

            // Assert
            result.IsSuccess.Should().BeTrue();
            service.Balance.Should().Be(amount);
        }

        [Fact]
        public void UpdateBalance_ShouldFail_WhenAmountIsZeroOrNegative()
        {
            // Arrange
            var service = new WalletService();
            var amount = 0;

            // Act
            var result = service.UpdateBalance(amount);

            // Assert
            result.IsSuccess.Should().BeFalse();
        }
    }
}
