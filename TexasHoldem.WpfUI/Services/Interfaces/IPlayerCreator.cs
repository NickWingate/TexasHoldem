using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.WpfUI.Services.Interfaces
{
	public interface IPlayerCreator
	{
		List<Player> CreatePlayers(int quantity);
	}
}