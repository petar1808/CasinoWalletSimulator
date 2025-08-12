using FluentAssertions;
using CasinoWallet.Services;
using CasinoWallet.Models.Enum;

namespace CasinoWallet.Tests.Tests.Services
{
    public class CommandParserServiceTests
    {
        [Fact]
        public void ParseCommand_ShouldReturnValidCommand_WhenInputIsCorrect()
        {
            // Arrange
            var service = new CommandParserService();
            var input = "deposit 100";

            // Act
            var result = service.ParseCommand(input);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.CommandType.Should().Be(CommandType.Deposit);
            result.Data.Amount.Should().Be(100);
        }

        [Fact]
        public void ParseCommand_ShouldFail_WhenCommandIsEmpty()
        {
            // Arrange
            var service = new CommandParserService();
            var input = "";

            // Act
            var result = service.ParseCommand(input);

            // Assert
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void ParseCommand_ShouldFail_WhenCommandIsUnknown()
        {
            // Arrange
            var service = new CommandParserService();
            var input = "invalid 50";

            // Act
            var result = service.ParseCommand(input);

            // Assert
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void ParseCommand_ShouldFail_WhenAmountIsInvalid()
        {
            // Arrange
            var service = new CommandParserService();
            var input = "deposit abc";

            // Act
            var result = service.ParseCommand(input);

            // Assert
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void ParseCommand_ShouldFail_WhenTooManyArguments()
        {
            // Arrange
            var service = new CommandParserService();
            var input = "deposit 100 extra";

            // Act
            var result = service.ParseCommand(input);

            // Assert
            result.IsSuccess.Should().BeFalse();
        }
    }
}
