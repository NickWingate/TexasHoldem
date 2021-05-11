using System.Collections.Generic;
using TexasHoldem.Domain.Entities;
using TexasHoldem.WpfUI.Services.Interfaces;

namespace TexasHoldem.WpfUI.Services
{
	public class PlayerCreator : IPlayerCreator
	{
		public List<Player> CreatePlayers(int quantity)
		{
			var players = new List<Player>();
			int i = 0;
			for (; i < quantity; i++)
			{
				players.Add(new Player()
				{
					Name = $"Player{i+1}",
					ChipCount = 1000,
				});
			}

			return players;
		}
	}
}