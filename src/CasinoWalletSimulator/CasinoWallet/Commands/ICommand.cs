using CasinoWallet.Models.Enum;

namespace CasinoWallet.Commands
{
    public interface ICommand
    {
        void Execute();

        CommandType CommandType { get; }
    }
}
