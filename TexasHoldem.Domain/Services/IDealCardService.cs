using System.Collections.Generic;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Enums;

namespace TexasHoldem.Domain.Services
{
	public interface IDealCardService
	{
		void DealCards(List<Player> players, Deck deck, int cardsPerHand);
		void DealCommunityCards(Deck deck, List<Card> cards, int amount);
	}
}