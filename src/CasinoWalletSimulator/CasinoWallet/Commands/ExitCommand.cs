using CasinoWallet.Models.Enum;

namespace CasinoWallet.Commands
{
    public class ExitCommand : ICommand
    {
        public CommandType CommandType => CommandType.Exit;

        public void Execute()
        {
            Console.WriteLine("Thank you for playing! Hope to see you again soon.");
        }
    }
}
