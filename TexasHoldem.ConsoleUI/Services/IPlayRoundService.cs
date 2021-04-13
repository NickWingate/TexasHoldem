using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.ConsoleUI.Services
{
	public interface IPlayRoundService
	{
		void PlayRound(List<Player> players, Deck deck, Pot pot, int indexOfDealer, int blindAmount);
	}
}