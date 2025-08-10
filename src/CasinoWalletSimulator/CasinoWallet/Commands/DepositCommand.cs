using CasinoWallet.Models.Enum;
using CasinoWallet.Services;

namespace CasinoWallet.Commands
{
    public class DepositCommand : ICommand
    {
        private readonly WalletService _walletService;

        public DepositCommand(WalletService walletService)
        {
            _walletService = walletService;
        }

        public CommandType CommandType => CommandType.Deposit;

        public void Execute()
        {
            var amount = Console.ReadLine();

            if (decimal.TryParse(amount, out var parsedAmount))
            {
                _walletService.Deposit(parsedAmount);
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }
    }
}
