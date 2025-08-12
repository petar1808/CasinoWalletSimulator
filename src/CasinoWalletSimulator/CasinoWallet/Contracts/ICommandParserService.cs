using CasinoWallet.Models;

namespace CasinoWallet.Contracts
{
    public interface ICommandParserService
    {
        Result<CommandParserModel> ParseCommand(string? input);
    }
}
