using CasinoWallet.Contracts;
using CasinoWallet.Models;
using CasinoWallet.Models.Enum;

namespace CasinoWallet.Commands
{
    public class DepositCommand : ICommand
    {
        private readonly IWalletService _walletService;

        public DepositCommand(IWalletService walletService)
        {
            _walletService = walletService;
        }

        public CommandType CommandType => CommandType.Deposit;

        public Result Execute(decimal amount)
        {
            var result = _walletService.Deposit(amount);

            if (result.IsSuccess)
            {
                return new Result(true, $"Your deposit of ${amount:F2} was successful. Your current balance is: ${_walletService.Balance:F2}");
            }

            return result;
        }
    }
}
