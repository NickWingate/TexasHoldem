using TexasHoldem.Domain.Enums;

namespace TexasHoldem.Domain.Entities
{
	public class Card
	{
		public Card(Suit suit, Rank rank)
		{
			Rank = rank;
			Suit = suit;
		}
		public Card(Suit suit, int rank)
		{
			Rank = (Rank)rank;
			if (rank == 1)
			{
				Rank = Rank.Ace;
			}
			Suit = suit;
		}
		public Rank Rank { get; set; }
		public Suit Suit { get; set; }
		

		public override string ToString()
		{
			return $"{Rank} of {Suit}";
		}

		public static bool operator <(Card lhs, Card rhs)
		{
			if (lhs.Rank == rhs.Rank)
			{
				return (int) lhs.Suit < (int) rhs.Suit;
			}

			return (int) lhs.Rank < (int) rhs.Rank;
		}
		
		public static bool operator >(Card lhs, Card rhs)
		{
			if (lhs.Rank == rhs.Rank)
			{
				return (int) lhs.Suit > (int) rhs.Suit;
			}

			return (int) lhs.Rank > (int) rhs.Rank;
		}

		public static bool operator ==(Card lhs, Card rhs)
		{
			return lhs.Suit == rhs.Suit && 
			       lhs.Rank == rhs.Rank;
		}
		
		public static bool operator !=(Card lhs, Card rhs)
		{
			return lhs.Suit != rhs.Suit || 
			       lhs.Rank != rhs.Rank;
		}
	}
}