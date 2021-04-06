using System;
using System.Collections.Generic;
using TexasHoldem.ConsoleUI.Services;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.ConsoleUI
{
	// https://www.instructables.com/Learn-To-Play-Poker---Texas-Hold-Em-aka-Texas-Ho/
	public class TexasHoldem
	{
		private readonly IPlayerCreatorService _playerCreator;
		private readonly IDetermineDealerService _determineDealerService;
		public TexasHoldem(IPlayerCreatorService playerCreator)
		{
			_playerCreator = playerCreator;
		}

		
		
		public void Run()
		{
			var deck = new Deck();
			deck.Shuffle();
			var players = _playerCreator.CreatePlayers(2);

			var indexOfDealer = _determineDealerService.IndexOfDealer(players);
			
			while (!deck.IsEmpty)
			{
				Console.WriteLine("New round");
				PlayRound(players, deck, indexOfDealer);

			}
		}

		private void PlayRound(List<Player> players, Deck deck, object indexOfDealer)
		{
			var pot = 0;
			var communityCards = new List<Card>(5);
			BlindBets(players, indexOfDealer, blindAmount : 10, ref pot);
			DealCards(players, deck, indexOfDealer);
			ShowCards(players);
			
			PlayersAct(players, pot, indexOfDealer, firstRound: true);
			DealCommunityCard(3, communityCards);
			while (playing)
			{
				PlayersAct(players, pot, indexOfDealer, firstRound: false);
				DealCommunityCard(1, communityCards);
			}
			
		}
	}
}