using CasinoWallet.Models;
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

        public Result Execute(decimal amount)
        {
            var result = _walletService.Deposit(amount);

            if (result.IsSuccess)
            {
                return new Result(true, $"Your deposit of ${amount} was successful. Your current balance is: ${_walletService.Balance:F2}");
            }

            return result;
        }
    }
}
