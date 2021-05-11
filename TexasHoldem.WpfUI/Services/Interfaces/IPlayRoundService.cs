using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.WpfUI.Services.Interfaces
{
	public interface IPlayRoundService
	{
		Player PlayRound(List<Player> players, Deck deck, Pot pot, ICollection<Card> communityCards, int indexOfDealer, int blindAmount);
	}
}