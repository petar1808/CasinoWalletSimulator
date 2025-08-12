using CasinoWallet.Config;
using CasinoWallet.Models;
using CasinoWallet.Models.Enum;
using CasinoWallet.Services;
using Microsoft.Extensions.Options;

namespace CasinoWallet.Commands
{
    public class BetCommand : ICommand
    {
        private readonly WalletService _walletService;
        private readonly IGameService _gameService;
        private readonly BetSettings _betSettings;

        public BetCommand(WalletService walletService, GameService gameService, IOptions<BetSettings> betSettings)
        {
            _walletService = walletService;
            _gameService = gameService;
            _betSettings = betSettings.Value;
        }

        public CommandType CommandType => CommandType.Bet;

        public Result Execute(decimal amount)
        {
            if (amount < _betSettings.MinBet || amount > _betSettings.MaxBet)
            {
                return new Result(false, $"Bet must be between {_betSettings.MinBet} and {_betSettings.MaxBet}.");
            }

            if (_walletService.Balance < amount)
            {
                return new Result(false, $"Insufficient funds to place this bet.");
            }

            var resultWinAmount = _gameService.PlayRound(amount);

            if (!resultWinAmount.IsSuccess)
            {
                return new Result(false, resultWinAmount.Message);
            }

            var resultUpdateBalance = _walletService.UpdateBalance(amount, resultWinAmount.Data);

            if (resultWinAmount.Data > 0)
            {
                return new Result(true, $"Congrats - you won ${resultWinAmount.Data:F2}! Your current balanse is: ${_walletService.Balance:F2}");
            }
            else
            {
                return new Result(true, $"No luck this tume! Your current balanse is: ${_walletService.Balance:F2}");
            }
        }
    }
}
