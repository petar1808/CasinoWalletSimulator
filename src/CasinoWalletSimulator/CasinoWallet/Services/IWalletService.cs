namespace CasinoWallet.Services
{
    public interface IWalletService
    {
        decimal Balance { get;}

        void Deposit(decimal deposit);

        bool Withdraw(decimal withdrawAmount);
    }
}
