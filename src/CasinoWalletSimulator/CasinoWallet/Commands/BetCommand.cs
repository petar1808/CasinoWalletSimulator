using CasinoWallet.Config;
using CasinoWallet.Models.Enum;
using CasinoWallet.Services;
using Microsoft.Extensions.Options;

namespace CasinoWallet.Commands
{
    public class BetCommand : ICommand
    {
        private readonly WalletService _walletService;
        private readonly GameOfChanceService _gameOfChanceService;
        private readonly BetSettings _betSettings;

        public BetCommand(WalletService walletService, GameOfChanceService gameOfChanceService, IOptions<BetSettings> betSettings)
        {
            _walletService = walletService;
            _gameOfChanceService = gameOfChanceService;
            _betSettings = betSettings.Value;
        }

        public CommandType CommandType => CommandType.Bet;

        public void Execute()
        {
            var betAmount = Console.ReadLine();

            if (decimal.TryParse(betAmount, out var parsedBetAmount))
            {
                if (parsedBetAmount < _betSettings.MinBet || parsedBetAmount >= _betSettings.MaxBet)
                {
                    Console.WriteLine($"Bet must be between {_betSettings.MinBet} and {_betSettings.MaxBet}.");
                    return;
                }

                if (_walletService.Balance < parsedBetAmount)
                {
                    Console.WriteLine("Insufficient funds to place this bet.");
                    return;
                }

                _walletService.Withdraw(parsedBetAmount);

                var winAmount = _gameOfChanceService.PlayRound(parsedBetAmount);

                _walletService.Deposit(winAmount);
                
                if (winAmount > 0)
                {
                    Console.WriteLine($"Congrats - you won ${winAmount}! Your current balanse is: ${_walletService.Balance}");
                }
                else
                {
                    Console.WriteLine($"No luch this tume! Your current balanse is: ${_walletService.Balance}");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }
    }
}
