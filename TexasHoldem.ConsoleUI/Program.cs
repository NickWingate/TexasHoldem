using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TexasHoldem.ConsoleUI.Services;
using TexasHoldem.Domain.Services;

namespace TexasHoldem.ConsoleUI
{
	static class Program
	{
		static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			host.Services.GetRequiredService<TexasHoldem>().Run();
		}

		private static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureServices(services =>
				{
					services.AddTransient<TexasHoldem>();
					services.AddTransient<IPlayerCreatorService, PlayerCreatorService>();
					services.AddTransient<IDetermineDealerService, DetermineDealerService>();
					services.AddTransient<IBettingService, BettingService>();
					services.AddTransient<IPlayerParticipationService, PlayerParticipationService>();
					services.AddTransient<IDealCardService, DealCardService>();
					services.AddTransient<IPlayerActionService, PlayerActionService>();
					services.AddTransient<IAvailableActionProvider, AvailableActionProvider>();
					services.AddTransient<IHandEvaluator, HandEvaluator>();
					services.AddTransient<IWinnerService, WinnerService>();
					services.AddTransient<IPlayRoundService, PlayRoundService>();
					services.AddScoped<IConsoleOutputService, ConsoleOutputService>();
				});
		}
	}
}