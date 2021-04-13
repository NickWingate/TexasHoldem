using System.Collections.Generic;
using System.Linq;
using TexasHoldem.Domain.Enums;

namespace TexasHoldem.Domain.Entities
{
	public class HandRanking
	{
		public HandRank Rank { get; set; }
		public Card KickerCard { get; set; }

		public HandRanking(List<Card> hand, HandRank handRank)
		{
			KickerCard = hand.Max();
			Rank = handRank;
		}

		public override string ToString()
		{
			return Rank.ToString();
		}

		public static bool operator <(HandRanking lhs, HandRanking rhs)
		{
			if (lhs.Rank == rhs.Rank)
			{
				return lhs.KickerCard < rhs.KickerCard;
			}
			return (int) lhs.Rank < (int) rhs.Rank;
		}
		
		public static bool operator >(HandRanking lhs, HandRanking rhs)
		{
			if (lhs.Rank == rhs.Rank)
			{
				return lhs.KickerCard > rhs.KickerCard;
			}
			return (int) lhs.Rank > (int) rhs.Rank;
		}
	}
}