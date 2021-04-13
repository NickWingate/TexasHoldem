using System;
using TexasHoldem.Domain.Enums;

namespace TexasHoldem.Domain.Entities
{
	public class Card : IComparable
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
		public CardColor Color => DetermineCardColor();

		private CardColor DetermineCardColor()
		{
			return Suit == Suit.Spades || Suit == Suit.Clubs ? 
				CardColor.Black : CardColor.Red;
		}


		public override string ToString()
		{
			return $"{Rank} of {Suit}";
		}

		public int CompareTo(object obj)
		{
			if (obj is null) return 1;
			var otherCard = obj as Card;
			if (this < otherCard) return 1;
			if (this > otherCard) return -1;
			return 0;
		}

		protected bool Equals(Card other)
		{
			return Rank == other.Rank && Suit == other.Suit;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Card) obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine((int) Rank, (int) Suit);
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