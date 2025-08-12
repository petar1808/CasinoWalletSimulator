using CasinoWallet.Commands;
using CasinoWallet.Config;
using CasinoWallet.Contracts;
using CasinoWallet.Core;
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
                services.Configure<GameSettings>(
                  context.Configuration.GetSection(nameof(GameSettings)));

                services.Configure<BetSettings>(
                  context.Configuration.GetSection(nameof(BetSettings)));

                services.AddSingleton<Random>();

                services.AddSingleton<IGameService, GameService>();
                services.AddSingleton<IWalletService, WalletService>();
                services.AddSingleton<ICommandParserService, CommandParserService>();
                services.AddSingleton<IConsoleService, ConsoleService>();

                services.AddSingleton<ICommand, DepositCommand>();
                services.AddSingleton<ICommand, WithdrawCommand>();
                services.AddSingleton<ICommand, BetCommand>();

                services.AddSingleton<ICommandRegistry, CommandRegistry>();

                services.AddSingleton<GameRunner>();
            })
            .Build();

        var gameRunner = host.Services.GetRequiredService<GameRunner>();
        gameRunner.Run();
    }
}