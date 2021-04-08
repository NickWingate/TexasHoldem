using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.Domain.Services
{
	public interface IDetermineDealerService
	{
		int IndexOfDealer(List<Player> players);
	}
}