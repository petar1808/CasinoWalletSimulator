using CasinoWallet.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static void Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {

            })
            .Build();

        var gameRunner = host.Services.GetRequiredService<GameRunner>();
        gameRunner.Run();
    }
}
