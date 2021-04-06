using System;
using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.ConsoleUI.Services
{
	public class PlayerCreatorService : IPlayerCreatorService
	{
		public IList<Player> CreatePlayers(int quantity)
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
			Console.Write("Name: ");
			var name = Console.ReadLine();
			Console.Write("Amount of chips to start: ");
			var chips = Convert.ToInt32(Console.ReadLine());
			
			return new Player()
			{
				Name = name,
				ChipCount = chips,
			};
		}
	}
}