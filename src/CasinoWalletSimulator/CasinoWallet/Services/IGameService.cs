using CasinoWallet.Models;

namespace CasinoWallet.Services
{
    public interface IGameService
    {
        Result<decimal> PlayRound(decimal betAmount);
    }
}
