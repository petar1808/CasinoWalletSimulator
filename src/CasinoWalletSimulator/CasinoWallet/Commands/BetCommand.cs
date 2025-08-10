using CasinoWallet.Enum;

namespace CasinoWallet.Commands
{
    public class BetCommand : ICommand
    {
        public CommandType CommandType => CommandType.Bet;

        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}
