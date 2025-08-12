using CasinoWallet.Config;
using CasinoWallet.Services;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace CasinoWallet.Tests.Tests.Services
{
    public class GameServiceTests
    {
        private GameSettings LoadGameSettings()
        {
            return new GameSettings() { LosePercent = 50, WinUpTo2Percent = 40, MaxSmallWinMultiplier = 2.0, MaxBigWinMultiplier = 10.0 };
        }

        [Fact]
        public void PlayRound_ShouldReturnError_WhenBetAmountIsNegative()
        {
            // Arrange
            var gameSettings = Options.Create(LoadGameSettings());
            var randomMock = new Mock<Random>();
            var service = new GameService(randomMock.Object, gameSettings);
            var betAmount = -5;

            // Act
            var result = service.PlayRound(betAmount);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("positive number");
        }

        [Fact]
        public void PlayRound_ShouldReturnLoss_WhenChanceIsInLoseRange()
        {
            // Arrange
            var settings = LoadGameSettings();
            var randomMock = new Mock<Random>();
            randomMock.Setup(r => r.Next(0, 100)).Returns(20);
            var gameService = new GameService(randomMock.Object, Options.Create(settings));
            var betAmount = 10;

            // Act
            var result = gameService.PlayRound(betAmount);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(0);
        }

        [Fact]
        public void PlayRound_ShouldReturnSmallWin_WhenChanceInSmallWinRange()
        {
            // Arrange
            var settings = LoadGameSettings();
            var randomMock = new Mock<Random>();
            randomMock.Setup(r => r.Next(0, 100)).Returns(70);
            randomMock.Setup(r => r.NextDouble()).Returns(0.5);
            var gameService = new GameService(randomMock.Object, Options.Create(settings));
            var betAmount = 10;

            // Act
            var result = gameService.PlayRound(betAmount);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeApproximately(15, 0.001m);
        }

        [Fact]
        public void PlayRound_ShouldReturnBigWin_WhenChanceInBigWinRange()
        {
            // Arrange
            var settings = LoadGameSettings();
            var randomMock = new Mock<Random>();
            randomMock.Setup(r => r.Next(0, 100)).Returns(95);
            randomMock.Setup(r => r.NextDouble()).Returns(0.5);

            var gameService = new GameService(randomMock.Object, Options.Create(settings));

            var betAmount = 10;
            var expectedMultiplier = 2 + 0.5m * ((decimal)settings.MaxBigWinMultiplier - 2);
            var expectedWin = betAmount * expectedMultiplier;

            // Act
            var result = gameService.PlayRound(betAmount);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeApproximately(expectedWin, 0.001m);
        }
    }
}
