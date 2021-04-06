using System;
using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.ConsoleUI.Services
{
	//todo: this contains lots of business logic so should be in domain layer
	public class DetermineDealerService : IDetermineDealerService
	{
		public int IndexOfDealer(List<Player> players)
		{
			DealCards(players);

			var indexOfPlayerWithHighestCard = FindHighestCardIndex(players);
			ClearHands(players);
			return indexOfPlayerWithHighestCard;
		}

		private static void ClearHands(IEnumerable<Player> players)
		{
			foreach (var player in players)
			{
				player.Hand.Clear();
			}
		}

		private static int FindHighestCardIndex(List<Player> players)
		{
			var indexOfPlayerWithHighestCard = 0;

			for (int i = 1; i < players.Count; i++)
			{
				var highestCard = players[0].Hand[0];
				if (players[i].Hand[0] > highestCard)
				{
					highestCard = players[i].Hand[i];
					indexOfPlayerWithHighestCard = i;
				}
			}

			return indexOfPlayerWithHighestCard;
		}

		private static void DealCards(List<Player> players)
		{
			var deck = new Deck();
			deck.Shuffle();

			foreach (var player in players)
			{
				var card = deck.DrawCard();
				player.Hand.Add(card);
				Console.WriteLine($"{player.Name} drew a {card}");
			}
		}
	}
}