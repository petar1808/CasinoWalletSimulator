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
            var depositAmount = Console.ReadLine();

            if (decimal.TryParse(depositAmount, out var parsedDepositAmount))
            {
                _walletService.Deposit(parsedDepositAmount);

                Console.WriteLine($"Your deposit of ${parsedDepositAmount} was successful. Your current balance is: ${_walletService.Balance:F2}");
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }
    }
}
