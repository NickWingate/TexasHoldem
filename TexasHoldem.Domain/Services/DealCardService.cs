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
			CheckForEnoughCards(deck, players.Count, cardsPerHand);
			for (int i = 0; i < cardsPerHand; i++)
			{
				foreach (var player in players)
				{
					player.Hand.Add(deck.DrawCard());
				}
			}
		}

		private void CheckForEnoughCards(Deck deck, int playersCount, int cardsPerHand)
		{
			var cardsNeeded = playersCount * cardsPerHand;
			if (cardsNeeded > deck.Count)
			{
				throw new EmptyDeckException("There are not enough cards for another round");
			}
		}
	}
}