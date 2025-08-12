using CasinoWallet.Models;

namespace CasinoWallet.Contracts
{
    public interface IWalletService
    {
        decimal Balance { get;}

        Result Deposit(decimal deposit);

        Result Withdraw(decimal withdrawAmount);

        Result UpdateBalance(decimal amount);
    }
}
