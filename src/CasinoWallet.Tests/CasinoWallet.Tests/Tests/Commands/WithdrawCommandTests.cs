using CasinoWallet.Commands;
using CasinoWallet.Contracts;
using CasinoWallet.Models;
using FluentAssertions;
using Moq;

namespace CasinoWallet.Tests.Tests.Commands
{
    public class WithdrawCommandTests
    {
        [Fact]
        public void Execute_ShouldReturnSuccess_WhenWithdrawSucceeds()
        {
            // Arrange
            var walletMock = new Mock<IWalletService>();
            walletMock.Setup(w => w.Withdraw(50)).Returns(new Result(true, string.Empty));
            walletMock.Setup(w => w.Balance).Returns(100);

            var command = new WithdrawCommand(walletMock.Object);

            decimal amount = 50;
            // Act
            var result = command.Execute(amount);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Message.Should().Contain($"Your withdrawal of ${amount:F2} was successful");
        }

        [Fact]
        public void Execute_ShouldReturnFailure_WhenWithdrawFails()
        {
            // Arrange
            var walletMock = new Mock<IWalletService>();
            walletMock.Setup(w => w.Withdraw(50)).Returns(new Result(false, "Insufficient funds"));

            var command = new WithdrawCommand(walletMock.Object);

            // Act
            var result = command.Execute(50);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Insufficient funds");
        }
    }
}
