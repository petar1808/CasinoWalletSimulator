using CasinoWallet.Models.Enum;

namespace CasinoWallet.Models
{
    public class CommandParserModel
    {
        public CommandParserModel(CommandType commandType, decimal amount)
        {
            CommandType = commandType;
            Amount = amount;
        }

        public CommandType CommandType { get; set; }

        public decimal Amount { get; set; }
    }
}
