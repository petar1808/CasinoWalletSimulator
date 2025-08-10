using CasinoWallet.Enum;

namespace CasinoWallet.Commands
{
    public class ExitCommand : ICommand
    {
        public CommandType CommandType => CommandType.Exit;

        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}
