using CasinoWallet.Models.Enum;

namespace CasinoWallet.Commands
{
    public class BetCommand : ICommand
    {
        public CommandType CommandType => CommandType.Bet;

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
