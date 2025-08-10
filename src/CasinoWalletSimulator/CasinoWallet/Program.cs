using CasinoWallet.Commands;
using CasinoWallet.Core;
using CasinoWallet.Models;
using CasinoWallet.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ICommand = CasinoWallet.Commands.ICommand;

class Program
{
    static void Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<GameRunner>();
                services.AddSingleton<CommandRegistry>();

                services.AddSingleton<PlayerWallet>();
                services.AddSingleton<WalletService>();

                services.AddTransient<ICommand, DepositCommand>();
                services.AddTransient<ICommand, WithdrawCommand>();
                services.AddTransient<ICommand, ExitCommand>();
            })
            .Build();

        var gameRunner = host.Services.GetRequiredService<GameRunner>();
        gameRunner.Run();
    }
}