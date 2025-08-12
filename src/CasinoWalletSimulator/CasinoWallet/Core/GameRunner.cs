using CasinoWallet.Contracts;

namespace CasinoWallet.Core
{
    public class GameRunner
    {
        private const string ExitCommand = "exit";
        private const string ExitMessage = "Thank you for playing! Hope to see you again soon.";

        private readonly ICommandRegistry _commandRegistry;
        private readonly ICommandParserService _commandParserService;
        private readonly IConsoleService _consoleService;

        public GameRunner(ICommandRegistry commandRegistry, ICommandParserService commandParserService, IConsoleService consoleService)
        {
            _commandRegistry = commandRegistry;
            _commandParserService = commandParserService;
            _consoleService = consoleService;
        }

        public void Run()
        {
            while (true)
            {
                _consoleService.WriteLine("\nPlease submit Action: ");
                var input = _consoleService.ReadLine();

                if (string.Equals(input?.Trim(), ExitCommand, StringComparison.OrdinalIgnoreCase))
                {
                    _consoleService.WriteLine(ExitMessage);
                    break;
                }

                var commandParserResult = _commandParserService.ParseCommand(input);

                if (commandParserResult.IsSuccess)
                {
                    if (_commandRegistry.TryGetCommand(commandParserResult.Data.CommandType, out var command))
                    {
                        try
                        {
                            var result = command.Execute(commandParserResult.Data.Amount);
                            _consoleService.WriteLine(result.Message);
                        }
                        catch (Exception ex)
                        {
                            _consoleService.WriteLine($"An unexpected error occurred during command execution: {ex.Message}");
                        }
                    }
                    else
                    {
                        _consoleService.WriteLine("Unsupported command.");
                    }
                }
                else
                {
                    _consoleService.WriteLine(commandParserResult.Message);
                }
            }
        }
    }
}
