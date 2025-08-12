using CasinoWallet.Models;
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

        public Result Execute(decimal amount)
        {
            var result = _walletService.Withdraw(amount);

            if (result.IsSuccess)
            {
                return new Result(true, $"Your withdrawal of ${amount}. Your current balance is: ${_walletService.Balance:F2}.");
            }

            return result;
        }
    }
}
