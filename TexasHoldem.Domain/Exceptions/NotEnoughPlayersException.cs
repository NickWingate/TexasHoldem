using System;
using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.Domain.Exceptions
{
	public class NotEnoughPlayersException : Exception
	{
		public NotEnoughPlayersException(ICollection<Player> players)
			: base($"Must have at least 2 players, had {players.Count}")
		{
		}
	}
}