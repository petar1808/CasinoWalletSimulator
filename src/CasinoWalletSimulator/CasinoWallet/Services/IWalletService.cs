using CasinoWallet.Models;

namespace CasinoWallet.Services
{
    public interface IWalletService
    {
        decimal Balance { get;}

        Result Deposit(decimal deposit);

        Result Withdraw(decimal withdrawAmount);

        Result UpdateBalance(decimal betAmount, decimal winAmount);
    }
}
