using System.Collections.Generic;

namespace TexasHoldem.Domain.Entities
{
	public class Player
	{
		public string Name { get; set; }
		public List<Card> Hand { get; set; } = new List<Card>();
		public int ChipCount { get; set; }
	}
}