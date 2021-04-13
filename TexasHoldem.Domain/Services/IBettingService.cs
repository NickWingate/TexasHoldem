using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.Domain.Services
{
	public interface IBettingService
	{
		void BlindBets(Player smallBlind, Player bigBlind, int blindAmount, Pot pot);
		(Player, Player) DetermineBlindBetPlayers(List<Player> players, int indexOfDealer);

		int DetermineSmallBlindPrice(int bigBlind);
	}
}