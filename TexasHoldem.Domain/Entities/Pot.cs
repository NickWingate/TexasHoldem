namespace TexasHoldem.Domain.Entities
{
	public class Pot
	{
		public int Chips { get; set; } = 0;
		public int CurrentBet { get; set; } = 0;
		public int Type { get; set; }

		public void Bet(int amount)
		{
			Chips += amount;
			CurrentBet = amount;
		}

		/// <summary>
		/// Raise the bet to a new amount
		/// </summary>
		/// <param name="amount"></param>
		public void Raise(int amount)
		{
			Chips += amount;
		}

		public int Clear()
		{
			var chips = Chips;
			Chips = 0;
			CurrentBet = 0;
			return chips;
		}
	}
}