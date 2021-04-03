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
			Suit = suit;
		}
		public Rank Rank { get; set; }
		public Suit Suit { get; set; }
		

		public override string ToString()
		{
			return $"{Rank} of {Suit}";
		}
	}
}