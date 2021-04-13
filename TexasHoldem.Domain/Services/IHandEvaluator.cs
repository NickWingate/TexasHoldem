using System.Collections.Generic;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Enums;

namespace TexasHoldem.Domain.Services
{
	public interface IHandEvaluator
	{
		HandRanking BestHand(List<Card> hand, List<Card> communityCards);
		public HandRank DetermineBestRank(List<Card> cards);

		bool ContainsRoyalFlush(List<Card> cards);
		bool ContainsStraight(List<Card> cards);
		bool ContainsFlush(List<Card> cards);
		bool ContainsXOfAKind(int x, List<Card> cards);
		bool ContainsFullHouse(List<Card> cards);
		bool ContainsTwoPair(List<Card> cards);
	}
}