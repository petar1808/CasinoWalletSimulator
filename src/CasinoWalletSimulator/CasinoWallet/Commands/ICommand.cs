using CasinoWallet.Enum;

namespace CasinoWallet.Commands
{
    public interface ICommand
    {
        Task Execute();

        CommandType CommandType { get; }
    }
}
