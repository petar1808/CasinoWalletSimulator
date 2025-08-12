using CasinoWallet.Models;

namespace CasinoWallet.Contracts
{
    public interface IGameService
    {
        Result<decimal> PlayRound(decimal betAmount);
    }
}
