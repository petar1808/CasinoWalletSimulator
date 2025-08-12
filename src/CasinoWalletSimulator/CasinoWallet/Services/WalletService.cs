using CasinoWallet.Models;

namespace CasinoWallet.Services
{
    public class WalletService : IWalletService
    {
        private decimal _balance;

        public WalletService(decimal initialBalance = 0)
        {
            _balance = initialBalance;
        }

        public decimal Balance => _balance;

        public void Deposit(decimal amount)
        {
            _balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (_balance >= amount)
            {
                _balance -= amount;
                return true;
            }

            return false;
        }
    }
}
