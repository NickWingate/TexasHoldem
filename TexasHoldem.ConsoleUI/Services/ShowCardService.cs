using System;
using System.Collections.Generic;
using Spectre.Console;
using TexasHoldem.ConsoleUI.Extensions;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.ConsoleUI.Services
{
	public class ShowCardService : IShowCardService
	{
		public void ShowCards(List<Player> players)
		{
			foreach (var player in players)
			{
				ShowCards(player);
			}
		}

		private void ShowCards(Player player)
		{
			Console.Write($"Press enter to see {player.Name}'s cards");
			Console.ReadLine();
			foreach (var card in player.Hand)
			{
				AnsiConsole.MarkupLine(card.ToMarkupString());
			}
			Console.WriteLine("Press enter to clear cards from screen");
			Console.ReadLine();
            Console.Clear();
		}
	}
}