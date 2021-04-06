using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.ConsoleUI.Services
{
	public interface IPlayerCreatorService
	{
		IList<Player> CreatePlayers(int quantity);
	}
}