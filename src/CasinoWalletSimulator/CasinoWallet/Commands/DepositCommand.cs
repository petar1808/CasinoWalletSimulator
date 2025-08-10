using CasinoWallet.Enum;

namespace CasinoWallet.Commands
{
    public class DepositCommand : ICommand
    {
        public CommandType CommandType => CommandType.Deposit;

        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}
