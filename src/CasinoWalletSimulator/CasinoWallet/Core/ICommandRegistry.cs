using CasinoWallet.Commands;
using CasinoWallet.Models.Enum;

namespace CasinoWallet.Core
{
    public interface ICommandRegistry
    {
        bool TryGetCommand(CommandType name, out ICommand command);
    }
}
