using CasinoWallet.Commands;
using CasinoWallet.Models.Enum;

namespace CasinoWallet.Core
{
    public class CommandRegistry : ICommandRegistry
    {
        private readonly Dictionary<CommandType, ICommand> _commands;

        public CommandRegistry(IEnumerable<ICommand> commands)
        {
            _commands = commands.ToDictionary(c => c.CommandType, c => c);
        }

        public bool TryGetCommand(CommandType name, out ICommand command) => _commands.TryGetValue(name, out command);
    }
}
