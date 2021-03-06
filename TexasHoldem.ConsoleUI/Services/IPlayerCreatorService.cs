using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.ConsoleUI.Services
{
	public interface IPlayerCreatorService
	{
		List<Player> CreatePlayers(int quantity);
	}
}