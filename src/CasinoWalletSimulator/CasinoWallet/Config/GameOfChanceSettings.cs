namespace CasinoWallet.Config
{
    public class GameOfChanceSettings
    {
        public int LoseChancePercent { get; set; }

        public int WinUpTo2ChancePercent { get; set; }

        public double MaxSmallWinMultiplier { get; set; }

        public double MaxBigWinMultiplier { get; set; }
    }
}
