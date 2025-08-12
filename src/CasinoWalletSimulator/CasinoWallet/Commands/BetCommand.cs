using CasinoWallet.Config;
using CasinoWallet.Contracts;
using CasinoWallet.Models;
using CasinoWallet.Models.Enum;
using Microsoft.Extensions.Options;

namespace CasinoWallet.Commands
{
    public class BetCommand : ICommand
    {
        private readonly IWalletService _walletService;
        private readonly IGameService _gameService;
        private readonly BetSettings _betSettings;

        public BetCommand(IWalletService walletService, IGameService gameService, IOptions<BetSettings> betSettings)
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

            var playResult = _gameService.PlayRound(amount);

            if (!playResult.IsSuccess)
            {
                return new Result(false, playResult.Message);
            }

            var balanceUpdateAmount = playResult.Data - amount;

            if (playResult.Data > 0)
            {
                var updateBalanceResult = _walletService.UpdateBalance(balanceUpdateAmount);

                if (!updateBalanceResult.IsSuccess)
                {
                    return new Result(false, updateBalanceResult.Message);
                }

                return new Result(true, $"Congrats - you won ${playResult.Data:F2}! Your current balanse is: ${_walletService.Balance:F2}");
            }
            else
            {
                var withrawResult = _walletService.Withdraw(amount);

                if (!withrawResult.IsSuccess)
                {
                    return withrawResult;
                }

                return new Result(true, $"No luck this time! Your current balanse is: ${_walletService.Balance:F2}");
            }
        }
    }
}
