using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.Domain.Services
{
	public interface IDealCardService
	{
		void DealCards(List<Player> players, Deck deck, int cardsPerHand);
	}
}