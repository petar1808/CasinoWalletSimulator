using CasinoWallet.Models.Enum;

namespace CasinoWallet.Core
{
    public class GameRunner
    {
        private readonly CommandRegistry _commandRegistry;

        public GameRunner(CommandRegistry commandRegistry)
        {
            _commandRegistry = commandRegistry;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\nPlease submit Action: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out _))
                {
                    Console.WriteLine("Numeric input is not allowed.");
                }
                else if (System.Enum.TryParse<CommandType>(input, true, out var commandType))
                {
                    if (_commandRegistry.TryGetCommand(commandType, out var command))
                    {
                        command.Execute();
                        if (commandType == CommandType.Exit)
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Command not implemented.");
                    }
                }
                else
                {
                    Console.WriteLine("Unknown command.");
                }
            }
        }
    }
}
