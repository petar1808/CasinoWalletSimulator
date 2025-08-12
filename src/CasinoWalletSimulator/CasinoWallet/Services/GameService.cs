using CasinoWallet.Config;
using CasinoWallet.Contracts;
using CasinoWallet.Models;
using Microsoft.Extensions.Options;

namespace CasinoWallet.Services
{
    public class GameService : IGameService
    {
        private Random _random;
        private readonly GameSettings _gameSettings;

        public GameService(Random random, IOptions<GameSettings> gameSettings)
        {
            _random = random;
            _gameSettings = gameSettings.Value;
        }

        public Result<decimal> PlayRound(decimal betAmount)
        {
            if (betAmount <= 0)
            {
                return new Result<decimal>(false, "Bet amount must be a positive number!", 0);
            }

            var chance = _random.Next(0, 100);

            decimal winAmount = 0;

            if (chance < _gameSettings.LosePercent)
            {
                winAmount = 0;
            }
            else if (chance < _gameSettings.LosePercent + _gameSettings.WinUpTo2Percent)
            {
                double multiplier = 1 + _random.NextDouble() * (_gameSettings.MaxSmallWinMultiplier - 1);
                winAmount = betAmount * (decimal)multiplier;
            }
            else
            {
                double multiplier = 2 + _random.NextDouble() * (_gameSettings.MaxBigWinMultiplier - 2);
                winAmount = betAmount * (decimal)multiplier;
            }

            return new Result<decimal>(true, string.Empty, winAmount);
        }
    }
}
