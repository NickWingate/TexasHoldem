using Spectre.Console;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Enums;

namespace TexasHoldem.ConsoleUI.Extensions
{
	public static class CardExtensions
	{
		public static string ToMarkupString(this Card card)
		{
			var color = card.Color == CardColor.Red ? "red" : "white";
			return  $"[{color} bold] {card} [/]";
		}
	}
}