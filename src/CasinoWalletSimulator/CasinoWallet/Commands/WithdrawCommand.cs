using CasinoWallet.Models.Enum;
using CasinoWallet.Services;

namespace CasinoWallet.Commands
{
    public class WithdrawCommand : ICommand
    {
        private readonly WalletService _walletService;

        public WithdrawCommand(WalletService walletService)
        {
            _walletService = walletService;
        }

        public CommandType CommandType => CommandType.Withdraw;

        public void Execute()
        {
            var withdrawAmount = Console.ReadLine();

            if (decimal.TryParse(withdrawAmount, out var parsedWithdrawAmount))
            {
                if (_walletService.Withdraw(parsedWithdrawAmount))
                {
                    Console.WriteLine($"Your withdrawal of ${parsedWithdrawAmount}. Your current balance is: ${_walletService.Balance:F2}.");
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }
    }
}
