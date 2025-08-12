using CasinoWallet.Config;
using CasinoWallet.Models;
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

        public Result<decimal> PlayRound(decimal betAmount)
        {
            try
            {
                if (betAmount <= 0)
                {
                    return new Result<decimal>(false, "Bet amount must be a positive number!", 0);
                }

                var chance = _random.Next(0, 100);

                var winAmount = 0M;

                if (chance < _gameOfChanceSettings.LoseChancePercent)
                {
                    winAmount = 0;
                }
                else if (chance < _gameOfChanceSettings.LoseChancePercent + _gameOfChanceSettings.WinUpTo2ChancePercent)
                {
                    double multiplier = 1 + _random.NextDouble() * (_gameOfChanceSettings.MaxSmallWinMultiplier - 1);
                    winAmount = betAmount * (decimal)multiplier;
                }
                else
                {
                    double multiplier = 2 + _random.NextDouble() * (_gameOfChanceSettings.MaxBigWinMultiplier - 2);
                    winAmount = betAmount * (decimal)multiplier;
                }

                return new Result<decimal>(true, null!, winAmount);
            }
            catch (Exception)
            {
                return new Result<decimal>(false, $"An unexpected error occurred while playing the round", 0);
            }
        }
    }
}
