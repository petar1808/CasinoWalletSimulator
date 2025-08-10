using CasinoWallet.Models;

namespace CasinoWallet.Services
{
    public class WalletService : PlayerWallet
    {
        private readonly PlayerWallet _wallet;

        public WalletService(PlayerWallet wallet)
        {
            _wallet = wallet;
        }

        public decimal Balance => _wallet.Balance;

        public void Deposit(decimal deposit)
        {
            _wallet.Balance += deposit;
        }

        public bool Withdraw(decimal withdrawAmount)
        {
            if (Balance >= withdrawAmount)
            {
                _wallet.Balance -= withdrawAmount;
                return true;
            }

            return false;
        }
    }
}
