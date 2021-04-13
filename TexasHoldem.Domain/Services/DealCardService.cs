using System;
using System.Collections.Generic;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Exceptions;

namespace TexasHoldem.Domain.Services
{
	public class DealCardService : IDealCardService
	{
		public void DealCards(List<Player> players, Deck deck, int cardsPerHand)
		{
			CheckForEnoughCards(deck, players.Count * cardsPerHand);
			for (int i = 0; i < cardsPerHand; i++)
			{
				foreach (var player in players)
				{
					player.Hand.Add(deck.DrawCard());
				}
			}
		}

		public void DealCommunityCards(Deck deck, List<Card> cards, int amount)
		{
			CheckForEnoughCards(deck, amount);
			for (int i = 0; i < amount; i++)
			{
				cards.Add(deck.DrawCard());
			}
		}

		private void CheckForEnoughCards(Deck deck, int cardsNeeded)
		{
			if (cardsNeeded > deck.Count)
			{
				throw new EmptyDeckException("There are not enough cards to deal");
			}
		}
	}
}