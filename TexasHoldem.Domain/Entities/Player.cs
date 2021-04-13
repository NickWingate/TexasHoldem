using System.Collections.Generic;
using TexasHoldem.Domain.Enums;
using TexasHoldem.Domain.Services;

namespace TexasHoldem.Domain.Entities
{
	public class Player
	{
		public string Name { get; set; }
		public List<Card> Hand { get; set; } = new List<Card>();
		public int ChipCount { get; set; }
		public HandRanking BestHand { get; set; }	
		public int CurrentBet { get; set; }

		public void AddToPot(int amount, Pot pot)
		{
			ChipCount -= amount;
			CurrentBet = amount;
			pot.Bet(amount);
		}

		public void ClearHand()
		{
			Hand.Clear();
			BestHand = null;
			CurrentBet = 0;
		}

		public override string ToString()
		{
			return Name;
		}

		public void WinChips(Pot pot)
		{
			ChipCount += pot.Clear();
		}
	}
}