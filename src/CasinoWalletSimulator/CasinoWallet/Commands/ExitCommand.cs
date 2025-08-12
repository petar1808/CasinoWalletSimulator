using CasinoWallet.Models;
using CasinoWallet.Models.Enum;

namespace CasinoWallet.Commands
{
    public class ExitCommand : ICommand
    {
        public CommandType CommandType => CommandType.Exit;

        public Result Execute(decimal amount)
        {
            return new Result(true, "Thank you for playing! Hope to see you again soon.");
        }
    }
}
