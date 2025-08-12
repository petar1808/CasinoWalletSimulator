using CasinoWallet.Contracts;
using CasinoWallet.Helper;
using CasinoWallet.Models;
using CasinoWallet.Models.Enum;

namespace CasinoWallet.Services
{
    public class CommandParserService : ICommandParserService
    {
        public Result<CommandParserModel> ParseCommand(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new Result<CommandParserModel>(false, "Action cannot be empty. Please enter a valid command.", 
                    new CommandParserModel(default, default));
            }

            var inputs = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (inputs.Count() > 2)
            {
                return new Result<CommandParserModel>(false, "Invalid command format. Use: <command> <amount>.",
                  new CommandParserModel(default, default));
            }

            var commandString = inputs[0];

            if (!EnumHelper.TryParseNonNumeric<CommandType>(commandString, true, out var commandType))
            {
                return new Result<CommandParserModel>(false, "Unknown command. Supported commands are: bet, deposit, withdraw, exit",
                 new CommandParserModel(default, default));
            }

            if (inputs.Count() == 1)
            {
                return new Result<CommandParserModel>(false, "Invalid command format. Use: <command> <amount>.",
                 new CommandParserModel(default, default));
            }

            var amountString = inputs[1];

            if (!decimal.TryParse(amountString, out var amount))
            {
                return new Result<CommandParserModel>(false, "Invalid amount! Please enter a valid number.",
                 new CommandParserModel(default, default));
            }

            return new Result<CommandParserModel>(true, string.Empty, new CommandParserModel(commandType, amount));
        }
    }
}
