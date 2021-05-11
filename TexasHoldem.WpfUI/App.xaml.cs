using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TexasHoldem.WpfUI.Services;
using TexasHoldem.WpfUI.Services.Interfaces;
using TexasHoldem.WpfUI.ViewModels;

namespace TexasHoldem.WpfUI
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{

		private void OnStartup(object sender, StartupEventArgs e)
		{
			var serviceProvider = CreateServiceProvider();

			var window = new MainWindow();
			window.DataContext = serviceProvider.GetRequiredService<MainViewModel>();
			window.Show();
		}

		private IServiceProvider CreateServiceProvider()
		{
			var services = new ServiceCollection();

			services.AddSingleton<ICardCodeService, CardCodeService>();
			services.AddSingleton<IPlayerCreator, PlayerCreator>();
			services.AddSingleton<IPlayRoundService, PlayRoundService>();
			services.AddScoped<MainViewModel>();

			return services.BuildServiceProvider();
		}
	}
}