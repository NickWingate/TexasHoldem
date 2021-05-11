using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using TexasHoldem.Domain.Entities;
using TexasHoldem.WpfUI.Services;
using TexasHoldem.WpfUI.Services.Interfaces;

namespace TexasHoldem.WpfUI.Converters
{
	public class CardToImageSourceConverter : IValueConverter
	{
		private readonly ICardCodeService _cardCodeService;

		public CardToImageSourceConverter()
		{
			_cardCodeService = new CardCodeService();
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
			var cardCode = _cardCodeService.GetCode((Card) value);
			var fileName = $"{projectPath}\\Resources\\Cards\\{cardCode}.png";

			return new ImageSourceConverter().ConvertFrom(fileName);
		}
		

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		private string GetCode(Card card)
		{
			var rankCode = (int) card.Rank <= 10
				? ((int) card.Rank).ToString()
				: card.Rank.ToString()[0].ToString();
			var suitCode = card.Suit.ToString()[0];

			return $"{rankCode}{suitCode}";
		}
	}
}