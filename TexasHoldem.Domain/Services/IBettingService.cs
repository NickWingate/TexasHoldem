using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.Domain.Services
{
	public interface IBettingService
	{
		void BlindBets(List<Player> players, int indexOfDealer, int blindAmount, ref int pot);

		int DetermineSmallBlindPrice(int bigBlind);
	}
}