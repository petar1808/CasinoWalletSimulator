using CasinoWallet.Contracts;
using CasinoWallet.Models;
using CasinoWallet.Models.Enum;

namespace CasinoWallet.Commands
{
    public class WithdrawCommand : ICommand
    {
        private readonly IWalletService _walletService;

        public WithdrawCommand(IWalletService walletService)
        {
            _walletService = walletService;
        }

        public CommandType CommandType => CommandType.Withdraw;

        public Result Execute(decimal amount)
        {
            var result = _walletService.Withdraw(amount);

            if (result.IsSuccess)
            {
                return new Result(true, $"Your withdrawal of ${amount:F2} was successful. Your current balance is: ${_walletService.Balance:F2}.");
            }

            return result;
        }
    }
}
