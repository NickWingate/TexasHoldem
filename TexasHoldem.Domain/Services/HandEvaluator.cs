using System;
using System.Collections.Generic;
using System.Linq;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Enums;
using TexasHoldem.Domain.Extensions;

namespace TexasHoldem.Domain.Services
{
	//https://en.wikipedia.org/wiki/List_of_poker_hands
	public class HandEvaluator : IHandEvaluator
	{
		public HandRanking BestHand(List<Card> hand, List<Card> communityCards)
		{
			var cards = hand.Concat(communityCards).ToList();
			var bestHand = new HandRanking(hand, DetermineBestRank(cards));
			return bestHand;
		}

		public HandRank DetermineBestRank(List<Card> cards)
		{
			var handRankingCheckers = new Dictionary<Func<List<Card>, bool>, HandRank>
			{
				{ContainsRoyalFlush, HandRank.RoyalFlush},
				{ContainsStraightFlush, HandRank.StraightFlush},
				{c => ContainsXOfAKind(4, c), HandRank.FourOfAKind},
				{ContainsFullHouse, HandRank.FullHouse},
				{ContainsFlush, HandRank.Flush},
				{ContainsStraight, HandRank.Straight},
				{c => ContainsXOfAKind(3, c), HandRank.FourOfAKind},
				{ContainsTwoPair, HandRank.TwoPair},
				{c => ContainsXOfAKind(2, c), HandRank.OnePair},
			};
			foreach (var handRankingChecker in handRankingCheckers)
			{
				if (handRankingChecker.Key(cards))
				{
					return handRankingChecker.Value;
				}
			}

			return HandRank.HighCard;
		}

		private bool ContainsStraightFlush(List<Card> cards)
		{
			return ContainsStraight(cards) && ContainsFlush(cards);
		}
		
		public bool ContainsRoyalFlush(List<Card> cards)
		{
			var royalRanks = new Rank[] { Rank.Ten, Rank.Jack, Rank.Queen, Rank.King, Rank.Ace };
			var royallyRankedCards = cards.Where(c => royalRanks.Contains(c.Rank)).ToList();
			return ContainsFlush(royallyRankedCards);
		}

		public bool ContainsFlush(List<Card> cards)
		{
			var suitFrequencyTable = new Dictionary<Suit, int>();
			var allSuits = (Suit[]) Enum.GetValues(typeof(Suit));
			foreach (var suit in allSuits)
			{
				var suitFrequency = cards.Count(card => card.Suit == suit);
				suitFrequencyTable.Add(suit, suitFrequency);
			}

			return suitFrequencyTable.ContainsAnyValueInRange(5, 8);
		}
		
		public bool ContainsStraight(List<Card> cards)
		{
			var ranks = cards.Select(card => (int) card.Rank).ToList();
			ranks.Sort();
			// in case of ace
			if (ranks.Contains(14))
			{
				ranks.Insert(0, 1);
			}
			
			var ranksIncrementingCount = 1;
			var maxRanksInOrder = 1;
			var previousRank = -1;
			foreach (var rank in ranks)
			{
				if (rank == previousRank + 1)
				{
					ranksIncrementingCount++;
					previousRank = rank;
					if (ranksIncrementingCount > maxRanksInOrder)
					{
						maxRanksInOrder = ranksIncrementingCount;
					}
					continue;
				}
				ranksIncrementingCount = 1;
				previousRank = rank;
			}

			return maxRanksInOrder >= 5;
		}

		public bool ContainsXOfAKind(int x, List<Card> cards)
		{
			var rankFrequencyTable = CountRankFrequency(cards);

			return rankFrequencyTable.ContainsValue(x);
		}

		public bool ContainsFullHouse(List<Card> cards)
		{
			return ContainsXOfAKind(3, cards) && ContainsXOfAKind(2, cards);
		}

		public bool ContainsTwoPair(List<Card> cards)
		{
			var rankFrequencyTable = CountRankFrequency(cards);

			var pairCount = rankFrequencyTable.Values.Count(frequency => frequency == 2);
			return pairCount == 2;
		}
		
		private static Dictionary<Rank, int> CountRankFrequency(List<Card> cards)
		{
			var rankFrequencyTable = new Dictionary<Rank, int>();
			var allRanks = (Rank[]) Enum.GetValues(typeof(Rank));
			foreach (var rank in allRanks)
			{
				var rankFrequency = cards.Count(card => card.Rank == rank);
				rankFrequencyTable.Add(rank, rankFrequency);
			}

			return rankFrequencyTable;
		}
	}
}