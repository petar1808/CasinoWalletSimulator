using CasinoWallet.Enum;

namespace CasinoWallet.Commands
{
    public class WithdrawCommand : ICommand
    {
        public CommandType CommandType => CommandType.Withdraw;

        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}
