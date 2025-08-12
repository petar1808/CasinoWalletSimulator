using CasinoWallet.Helper;
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
                try
                {
                    Console.WriteLine("\nPlease submit Action: ");
                    var input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("Error: Action cannot be empty. Please enter a valid command.");
                        continue;
                    }

                    var inputs = input.Split(' ');

                    if (inputs.Count() > 2)
                    {
                        Console.WriteLine("Error: Invalid command format. Use: <command> <amount>");
                        continue;
                    }

                    var commandInput = inputs[0];

                    var amountInput = int.Parse(inputs[1]);

                    if (EnumHelper.TryParseNonNumeric<CommandType>(commandInput, true, out var commandType))
                    {
                        if (_commandRegistry.TryGetCommand(commandType, out var command))
                        {
                            var result = command.Execute(amountInput);

                            Console.WriteLine(result.Message);

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
                catch (Exception)
                {
                    Console.WriteLine($"An unexpected error occurred:");
                    continue;
                }
            }
        }
    }
}
