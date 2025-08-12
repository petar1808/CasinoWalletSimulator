using CasinoWallet.Commands;
using CasinoWallet.Config;
using CasinoWallet.Contracts;
using CasinoWallet.Models;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace CasinoWallet.Tests.Tests.Commands
{
    public class BetCommandTests
    {
        private BetSettings LoadBetSettings()
        {
            return new BetSettings() { MinBet = 1, MaxBet = 10 };
        }

        private BetCommand CreateBetCommand(
            IWalletService walletService,
            IGameService gameService,
            BetSettings settings)
        {
            var options = Options.Create(settings);
            return new BetCommand(walletService, gameService, options);
        }

        [Fact]
        public void Execute_ShouldFail_WhenAmountBelowMinBet()
        {
            // Arrange
            var walletMock = new Mock<IWalletService>();
            var gameMock = new Mock<IGameService>();
            var settings = LoadBetSettings();

            var command = CreateBetCommand(walletMock.Object, gameMock.Object, settings);

            //Act
            var result = command.Execute(settings.MaxBet + 1);
            
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("Bet must be between");
        }

        [Fact]
        public void Execute_ShouldFail_WhenInsufficientBalance()
        {
            // Arrange
            var walletMock = new Mock<IWalletService>();
            walletMock.Setup(w => w.Balance).Returns(5);

            var gameMock = new Mock<IGameService>();

            var settings = LoadBetSettings();
            var command = CreateBetCommand(walletMock.Object, gameMock.Object, settings);

            //Act
            var result = command.Execute(6);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("Insufficient funds");
        }

        [Fact]
        public void Execute_ShouldReturnWinMessage_WhenGameResultIsPositive()
        {
            // Arrange
            var walletMock = new Mock<IWalletService>();
            walletMock.Setup(w => w.Balance).Returns(200);
            walletMock.Setup(w => w.UpdateBalance(It.IsAny<decimal>()))
                      .Returns(new Result(true, ""));

            var gameMock = new Mock<IGameService>();
            gameMock.Setup(g => g.PlayRound(5))
                    .Returns(new Result<decimal>(true, "", 20));

            var settings = LoadBetSettings();
            var command = CreateBetCommand(walletMock.Object, gameMock.Object, settings);

            //Act
            var result = command.Execute(5);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Message.Should().Contain("Congrats - you won");
        }

        [Fact]
        public void Execute_ShouldReturnLoseMessage_WhenGameResultIsZero()
        {
            // Arrange
            var currentBalance = 200m;

            var walletMock = new Mock<IWalletService>();
            walletMock.Setup(w => w.Balance).Returns(() => currentBalance);
            walletMock.Setup(w => w.Withdraw(It.IsAny<decimal>()))
                      .Returns<decimal>(amount =>
                      {
                          currentBalance -= amount;
                          return new Result(true, "");
                      });
            walletMock.Setup(w => w.UpdateBalance(It.IsAny<decimal>()))
                      .Returns(new Result(true, ""));

            var gameMock = new Mock<IGameService>();
            gameMock.Setup(g => g.PlayRound(It.IsAny<decimal>()))
                    .Returns<decimal>(amount => new Result<decimal>(true, "", 0m));

            var settings = LoadBetSettings();
            var command = new BetCommand(walletMock.Object, gameMock.Object, Options.Create(settings));

            // Act
            var result = command.Execute(5m);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Message.Should().Contain("No luck this time");

            walletMock.Verify(w => w.Withdraw(5m), Times.Once);
            walletMock.Verify(w => w.UpdateBalance(It.IsAny<decimal>()), Times.Never);
        }
    }
}