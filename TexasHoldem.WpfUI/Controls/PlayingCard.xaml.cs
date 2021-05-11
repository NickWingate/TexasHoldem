using System.Windows;
using System.Windows.Controls;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.WpfUI.Controls
{
	public partial class PlayingCard : UserControl
	{
		public PlayingCard()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty CardProperty = DependencyProperty.Register(
			"Card", typeof(Card), typeof(PlayingCard), new PropertyMetadata(default(Card)));

		public Card Card
		{
			get => (Card) GetValue(CardProperty);
			set => SetValue(CardProperty, value);
		}
	}
}