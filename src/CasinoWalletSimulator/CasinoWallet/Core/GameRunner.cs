using CasinoWallet.Enum;

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
            Console.WriteLine("Available commands:");
            foreach (var name in _commandRegistry.GetCommandNames())
            {
                Console.WriteLine($" - {name}");
            }

            while (true)
            {
                Console.Write("\nPlease submit Action: ");
                var input = Console.ReadLine();

                if (System.Enum.TryParse<CommandType>(input, true, out var commandType))
                {
                    if (_commandRegistry.TryGetCommand(commandType, out var command))
                    {
                        command.Execute();
                        if (commandType == CommandType.Exit)
                        {
                            Console.WriteLine("Thank you for playing! Hope to see you again soon.");
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
