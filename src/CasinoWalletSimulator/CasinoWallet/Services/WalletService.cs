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

        public Result Deposit(decimal amount)
        {
            if (amount <= 0) 
            {
                return new Result(false, "Deposit amount must be a positive number!");
            }

            _balance += amount;

            return new Result(true, null!);
        }

        public Result Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                return new Result(false, "Withdraw amount must be a positive number!");
            }

            if (_balance >= amount)
            {
                _balance -= amount;
                return new Result (true, null!);
            }

            return new Result(false, "Insufficient funds.");
        }

        public Result UpdateBalance(decimal betAmount, decimal winAmount)
        {
            if (betAmount <= 0)
            {
                return new Result(false, "Bet amount must be a positive number!");
            }

            if (betAmount > _balance)
            {
                return new Result(false, "Bet amount cannot be greater than your current balance!");
            }

            _balance = _balance - betAmount + winAmount;

            return new Result(true, null!);
        }
    }
}
