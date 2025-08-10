using CasinoWallet.Commands;
using CasinoWallet.Enum;

namespace CasinoWallet.Core
{
    public class CommandRegistry
    {
        private readonly Dictionary<CommandType, ICommand> _commands;

        public CommandRegistry(IEnumerable<ICommand> commands)
        {
            _commands = commands.ToDictionary(c => c.CommandType, c => c);
        }

        public bool TryGetCommand(CommandType name, out ICommand command) => _commands.TryGetValue(name, out command);

        public IEnumerable<CommandType> GetCommandNames() => _commands.Keys;
    }
}
