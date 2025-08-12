using CasinoWallet.Models;
using CasinoWallet.Models.Enum;

namespace CasinoWallet.Commands
{
    public interface ICommand
    {
        Result Execute(decimal amount);

        CommandType CommandType { get; }
    }
}
