using CasinoWallet.Contracts;
using CasinoWallet.Models;

namespace CasinoWallet.Services
{
    public class WalletService : IWalletService
    {
        private decimal _balance;

        public WalletService()
        {
            _balance = 0;
        }

        public decimal Balance => _balance;

        public Result Deposit(decimal amount)
        {
            if (amount <= 0) 
            {
                return new Result(false, "Deposit amount must be a positive number!");
            }

            _balance += amount;

            return new Result(true, string.Empty);
        }

        public Result Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                return new Result(false, "Withdraw amount must be a positive number!");
            }

            if (amount > _balance)
            {
                return new Result(false, "Insufficient funds.");
            }

            _balance -= amount;

            return new Result(true, string.Empty);
        }

        public Result UpdateBalance(decimal amount)
        {
            if (amount <= 0)
            {
                return new Result(false, "Update balance amount must be a positive number!");
            }

            _balance += amount;

            return new Result(true, string.Empty);
        }
    }
}
