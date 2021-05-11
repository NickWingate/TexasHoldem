using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.WpfUI.Controls
{
	public partial class PlayerControl : UserControl
	{
		public PlayerControl()
		{
			InitializeComponent();
		}




        public Player Player
        {
            get => (Player)GetValue(PlayerProperty);
            set => SetValue(PlayerProperty, value);
        }

        // Using a DependencyProperty as the backing store for Player.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlayerProperty =
            DependencyProperty.Register(nameof(Player), typeof(Player), typeof(PlayerControl), new PropertyMetadata(null));








    }
}