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
            Console.Write($"Withdraw: ");

            var amount = Console.ReadLine();

            if (decimal.TryParse(amount, out var parsedAmount))
            {
                if (_walletService.Withdraw(parsedAmount))
                {
                    Console.WriteLine($"Your withdrawal of ${_walletService.Balance}. Your current balance is: ${_walletService.Balance - parsedAmount}.");
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
