using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.Domain.Services
{
	public interface IWinnerService
	{
		Player FindWinner(List<Player> players, List<Card> communityCards);
	}
}