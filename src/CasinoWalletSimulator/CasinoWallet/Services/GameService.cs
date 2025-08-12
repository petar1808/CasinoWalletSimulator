using CasinoWallet.Config;
using Microsoft.Extensions.Options;

namespace CasinoWallet.Services
{
    public class GameService : IGameService
    {
        private Random _random;
        private readonly GameOfChanceSettings _gameOfChanceSettings;

        public GameService(Random random, IOptions<GameOfChanceSettings> gameOfChanceSettings)
        {
            _random = random;
            _gameOfChanceSettings = gameOfChanceSettings.Value;
        }

        public decimal PlayRound(decimal betAmount)
        {
            var chance = _random.Next(0, 100);

            var winAmount = 0M;

            if (chance < _gameOfChanceSettings.LoseChancePercent)
            {
                winAmount = 0;
                return winAmount;
            }
            else if (chance < _gameOfChanceSettings.LoseChancePercent + _gameOfChanceSettings.WinUpTo2ChancePercent)
            {
                double multiplier = 1 + _random.NextDouble() * (_gameOfChanceSettings.MaxSmallWinMultiplier - 1);
                winAmount = betAmount * (decimal)multiplier;
                return winAmount;
            }
            else
            {
                double multiplier = 2 + _random.NextDouble() * (_gameOfChanceSettings.MaxBigWinMultiplier - 2);
                winAmount = betAmount * (decimal)multiplier;
                return winAmount;
            }
        }
    }
}
