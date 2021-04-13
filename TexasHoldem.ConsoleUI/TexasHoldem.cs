using System;
using Spectre.Console;
using TexasHoldem.ConsoleUI.Services;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Services;

namespace TexasHoldem.ConsoleUI
{
	// https://www.instructables.com/Learn-To-Play-Poker---Texas-Hold-Em-aka-Texas-Ho/

	public class TexasHoldem
	{
		private readonly IPlayerCreatorService _playerCreator;
		private readonly IDetermineDealerService _determineDealerService;
		private readonly IPlayRoundService _playRoundService;
		private readonly IConsoleOutputService _consoleOutputService;

		public TexasHoldem(
			IPlayerCreatorService playerCreator, 
			IDetermineDealerService determineDealerService, 
			IPlayRoundService playRoundService, 
			IConsoleOutputService consoleOutputService)
		{
			_playerCreator = playerCreator;
			_determineDealerService = determineDealerService;
			_playRoundService = playRoundService;
			_consoleOutputService = consoleOutputService;
		}

		
		
		public void Run()
		{
			var deck = new Deck();
			deck.Shuffle();
			
			var players = _playerCreator.CreatePlayers(2);
			var pot = new Pot();

			var indexOfFirstDealer = _determineDealerService.IndexOfDealer(players);
			var indexOfDealer = indexOfFirstDealer;
			while (!deck.IsEmpty)
			{
				Console.WriteLine("New round");
				_consoleOutputService.OutputChips(players);
				_playRoundService.PlayRound(players, deck, pot, indexOfFirstDealer, 5);
				indexOfDealer = (indexOfDealer + 1) % players.Count;
			}
		}
	}
}