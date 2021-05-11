using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using TexasHoldem.Domain.Entities;
using TexasHoldem.WpfUI.Annotations;

namespace TexasHoldem.WpfUI.Controls
{
	public partial class CardsControl : UserControl , INotifyPropertyChanged
	{
		public CardsControl()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty CardsProperty = DependencyProperty.Register(
			nameof(Cards), typeof(ObservableCollection<Card>), typeof(CardsControl), new PropertyMetadata(default(ObservableCollection<Card>), OnCardsPropertyChanged));

        private static void OnCardsPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var cardsControl = dependencyObject as CardsControl;

            var oldCards = e.OldValue as INotifyCollectionChanged;
            var newCards = e.NewValue as INotifyCollectionChanged;

            if (oldCards is not null)
            {
                oldCards.CollectionChanged -= cardsControl.OnCardsCollectionChanged;
            }

            if (newCards is not null)
            {
                newCards.CollectionChanged += cardsControl.OnCardsCollectionChanged;
            }
            cardsControl.CreateCards();
        }

        private void OnCardsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CreateCards();
            //OnPropertyChanged(nameof(CardPanel));
        }

        public ObservableCollection<Card> Cards
		{
			get { return (ObservableCollection<Card>) GetValue(CardsProperty); }
			set { SetValue(CardsProperty, value); }
		}

        public void CreateCards()
        {
            CardPanel.Children.Clear();
            foreach (var card in Cards)
            {
                var cardControl = new PlayingCard
                {
                    Card = card
                };

                CardPanel.Children.Add(cardControl);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}