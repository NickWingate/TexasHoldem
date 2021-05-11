using TexasHoldem.Domain.Entities;
using TexasHoldem.WpfUI.Services.Interfaces;

namespace TexasHoldem.WpfUI.Services
{
	public class CardCodeService : ICardCodeService
	{
		public string GetCode(Card card)
		{
			var rankCode = (int) card.Rank <= 10
				? ((int) card.Rank).ToString()
				: card.Rank.ToString()[0].ToString();
			var suitCode = card.Suit.ToString()[0];

			return $"{rankCode}{suitCode}";
		}
	}
}