using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TexasHoldem.ConsoleUI.Services;

namespace TexasHoldem.ConsoleUI
{
	class Program
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
				});
		}
	}
}