using System;
using System.Collections.Generic;
using Spectre.Console;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.ConsoleUI.Services
{
	public class PlayerCreatorService : IPlayerCreatorService
	{
		public List<Player> CreatePlayers(int quantity)
		{
			var players = new List<Player>();
			for (int i = 0; i < quantity; i++)
			{
				players.Add(GetPlayerDetails());
			}

			return players;
		}

		private Player GetPlayerDetails()
		{
			var name = AnsiConsole.Ask<string>("Name:");
			var chips = AnsiConsole.Ask<int>("Amount of starting chips(recommended 100+):");
			
			return new Player
			{
				Name = name,
				ChipCount = chips,
			};
		}
	}
}