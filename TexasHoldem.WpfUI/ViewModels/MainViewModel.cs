using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Enums;
using TexasHoldem.WpfUI.Commands;
using TexasHoldem.WpfUI.Services.Interfaces;

namespace TexasHoldem.WpfUI.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
		private readonly IPlayerCreator _playerCreator;
		private readonly IPlayRoundService _playRoundService;
		
		public MainViewModel(
			IPlayerCreator playerCreator, 
			IPlayRoundService playRoundService)
		{
			_playerCreator = playerCreator;
			_playRoundService = playRoundService;

			_communityCards = new ObservableCollection<Card>();
			
			var deck = new Deck();
			deck.Shuffle();

			var players = InitializePlayers();
			
		}
		//todo: make command 
		// private void Play()
		// {
		// 	var pot = new Pot();
		//
		// 	while (!deck.IsEmpty)
		// 	{
		// 		_playRoundService.PlayRound(players, deck, pot, _communityCards, 0, 50);
		// 	}
		// }
		private List<Player> InitializePlayers()
		{
			var players = _playerCreator.CreatePlayers(2);
			Player1 = players[0];
			Player2 = players[1];
			return players;
		}

		private ObservableCollection<Card> _communityCards;

		public ObservableCollection<Card> CommunityCards
		{
			get =>  _communityCards;
			set
			{
				_communityCards = value;
				OnPropertyChanged();
			}
		}

		//todo: when player's property changes need to notifypropertychange
		public Player Player1 { get; set; }
		public Player Player2 { get; set; }

	}
}