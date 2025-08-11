using CasinoWallet.Models;

namespace CasinoWallet.Services
{
    public class WalletService : PlayerWallet
    {
        private readonly PlayerWallet _playerWallet;

        public WalletService(PlayerWallet playerWallet)
        {
            _playerWallet = playerWallet;
        }

        public decimal Balance => _playerWallet.Balance;

        public void Deposit(decimal deposit)
        {
            _playerWallet.Balance += deposit;
        }

        public bool Withdraw(decimal withdrawAmount)
        {
            if (Balance >= withdrawAmount)
            {
                _playerWallet.Balance -= withdrawAmount;
                return true;
            }

            return false;
        }
    }
}
