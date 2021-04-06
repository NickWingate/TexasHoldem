using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.ConsoleUI.Services
{
	public interface IDetermineDealerService
	{
		int IndexOfDealer(List<Player> players);
	}
}