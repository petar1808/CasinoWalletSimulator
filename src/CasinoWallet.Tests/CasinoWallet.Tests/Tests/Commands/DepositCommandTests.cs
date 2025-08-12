using CasinoWallet.Commands;
using CasinoWallet.Contracts;
using CasinoWallet.Models;
using FluentAssertions;
using Moq;

namespace CasinoWallet.Tests.Tests.Commands
{
    public class DepositCommandTests
    {
        [Fact]
        public void Execute_ShouldReturnSuccess_WhenDepositSucceeds()
        {
            // Arrange
            var walletMock = new Mock<IWalletService>();
            walletMock.Setup(w => w.Deposit(100)).Returns(new Result(true, ""));
            walletMock.Setup(w => w.Balance).Returns(150);

            var command = new DepositCommand(walletMock.Object);

            decimal amount = 100;

            // Act
            var result = command.Execute(amount);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Message.Should().Contain($"Your deposit of ${amount:F2} was successful");
        }

        [Fact]
        public void Execute_ShouldReturnFailure_WhenDepositFails()
        {
            // Arrange
            var walletMock = new Mock<IWalletService>();
            walletMock.Setup(w => w.Deposit(100)).Returns(new Result(false, "Deposit failed"));

            var command = new DepositCommand(walletMock.Object);

            // Act
            var result = command.Execute(100);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Deposit failed");
        }
    }
}
