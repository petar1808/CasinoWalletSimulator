using CasinoWallet.Commands;
using CasinoWallet.Config;
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
            .ConfigureServices((context, services) =>
            {
                services.Configure<GameOfChanceSettings>(
                  context.Configuration.GetSection(nameof(GameOfChanceSettings)));

                services.Configure<BetSettings>(
                  context.Configuration.GetSection(nameof(BetSettings)));

                services.AddSingleton<GameRunner>();
                services.AddSingleton<CommandRegistry>();
                services.AddSingleton<GameOfChanceService>();
                services.AddSingleton<Random>();

                services.AddSingleton<PlayerWallet>();
                services.AddSingleton<WalletService>();

                services.AddTransient<ICommand, DepositCommand>();
                services.AddTransient<ICommand, WithdrawCommand>();
                services.AddTransient<ICommand, ExitCommand>();
                services.AddTransient<ICommand, BetCommand>();
            })
            .Build();

        var gameRunner = host.Services.GetRequiredService<GameRunner>();
        gameRunner.Run();
    }
}