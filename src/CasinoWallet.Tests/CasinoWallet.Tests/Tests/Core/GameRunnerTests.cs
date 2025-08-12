using CasinoWallet.Commands;
using CasinoWallet.Contracts;
using CasinoWallet.Core;
using CasinoWallet.Models;
using CasinoWallet.Models.Enum;
using Moq;

namespace CasinoWallet.Tests.Tests.Core
{
    public class GameRunnerTests
    {
        [Fact]
        public void Run_WhenUserTypesExit_ShouldPrintExitMessage()
        {
            // Arrange
            var consoleMock = new Mock<IConsoleService>();
            consoleMock.SetupSequence(c => c.ReadLine())
                       .Returns("exit");

            var parserMock = new Mock<ICommandParserService>();
            var registryMock = new Mock<ICommandRegistry>();

            var runner = new GameRunner(registryMock.Object, parserMock.Object, consoleMock.Object);

            // Act
            runner.Run();

            // Assert
            consoleMock.Verify(c => c.WriteLine("Thank you for playing! Hope to see you again soon."), Times.Once);
        }

        [Fact]
        public void Run_WhenUserEntersValidCommand_ShouldExecuteCommandAndPrintResult()
        {
            // Arrange
            var commandMock = new Mock<ICommand>();
            commandMock.Setup(c => c.CommandType).Returns(CommandType.Bet);
            commandMock.Setup(c => c.Execute(It.IsAny<decimal>()))
                       .Returns(new Result(true, "Command executed"));

            var registryMock = new Mock<ICommandRegistry>();
            registryMock.Setup(r => r.TryGetCommand(CommandType.Bet, out It.Ref<ICommand>.IsAny))
                        .Returns((CommandType t, out ICommand c) => { c = commandMock.Object; return true; });

            var parserMock = new Mock<ICommandParserService>();
            parserMock.Setup(p => p.ParseCommand("bet 100"))
                      .Returns(new Result<CommandParserModel>(true, string.Empty, new CommandParserModel(CommandType.Bet, 100)));

            var consoleMock = new Mock<IConsoleService>();
            consoleMock.SetupSequence(c => c.ReadLine())
                       .Returns("bet 100")
                       .Returns("exit");

            var runner = new GameRunner(registryMock.Object, parserMock.Object, consoleMock.Object);

            // Act
            runner.Run();

            // Assert
            commandMock.Verify(c => c.Execute(100), Times.Once);
            consoleMock.Verify(c => c.WriteLine("Command executed"), Times.Once);
        }

        [Fact]
        public void Run_WhenCommandNotSupported_ShouldPrintUnsupportedCommandMessage()
        {
            // Arrange
            var registryMock = new Mock<ICommandRegistry>();
            registryMock.Setup(r => r.TryGetCommand(It.IsAny<CommandType>(), out It.Ref<ICommand>.IsAny))
                        .Returns(false);

            var parserMock = new Mock<ICommandParserService>();
            parserMock.Setup(p => p.ParseCommand("unsupported"))
                      .Returns(new Result<CommandParserModel>(true, string.Empty, new CommandParserModel(CommandType.Bet, 50)));

            var consoleMock = new Mock<IConsoleService>();
            consoleMock.SetupSequence(c => c.ReadLine())
                       .Returns("unsupported")
                       .Returns("exit");

            var runner = new GameRunner(registryMock.Object, parserMock.Object, consoleMock.Object);

            // Act
            runner.Run();

            // Assert
            consoleMock.Verify(c => c.WriteLine("Unsupported command."), Times.Once);
        }

        [Fact]
        public void Run_WhenCommandParserFails_ShouldPrintParserErrorMessage()
        {
            // Arrange
            var parserMock = new Mock<ICommandParserService>();
            parserMock.Setup(p => p.ParseCommand("invalid"))
                      .Returns(new Result<CommandParserModel>(false, "Invalid command", default!));

            var registryMock = new Mock<ICommandRegistry>();
            var consoleMock = new Mock<IConsoleService>();
            consoleMock.SetupSequence(c => c.ReadLine())
                       .Returns("invalid")
                       .Returns("exit");

            var runner = new GameRunner(registryMock.Object, parserMock.Object, consoleMock.Object);

            // Act
            runner.Run();

            // Assert
            consoleMock.Verify(c => c.WriteLine("Invalid command"), Times.Once);
        }

        [Fact]
        public void Run_WhenCommandThrowsException_ShouldPrintUnexpectedError()
        {
            // Arrange
            var commandMock = new Mock<ICommand>();
            commandMock.Setup(c => c.CommandType).Returns(CommandType.Bet);
            commandMock.Setup(c => c.Execute(It.IsAny<decimal>()))
                       .Throws(new InvalidOperationException("Test exception"));

            var registryMock = new Mock<ICommandRegistry>();
            registryMock.Setup(r => r.TryGetCommand(CommandType.Bet, out It.Ref<ICommand>.IsAny))
                        .Returns((CommandType t, out ICommand c) => { c = commandMock.Object; return true; });

            var parserMock = new Mock<ICommandParserService>();
            parserMock.Setup(p => p.ParseCommand("bet 100"))
                      .Returns(new Result<CommandParserModel>(true, string.Empty, new CommandParserModel(CommandType.Bet, 100)));

            var consoleMock = new Mock<IConsoleService>();
            consoleMock.SetupSequence(c => c.ReadLine())
                       .Returns("bet 100")
                       .Returns("exit");

            var runner = new GameRunner(registryMock.Object, parserMock.Object, consoleMock.Object);

            // Act
            runner.Run();

            // Assert
            consoleMock.Verify(c => c.WriteLine("An unexpected error occurred during command execution: Test exception"), Times.Once);
        }
    }
}
