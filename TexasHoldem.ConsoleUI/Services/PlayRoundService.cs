using System;
using System.Collections.Generic;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Enums;
using TexasHoldem.Domain.Services;

namespace TexasHoldem.ConsoleUI.Services
{
	public class PlayRoundService : IPlayRoundService
	{
		private readonly IBettingService _bettingService;
		private readonly IPlayerParticipationService _playerParticipationService;
		private readonly IDealCardService _dealCardService;
		private readonly IShowCardService _showCardService;
		private readonly IPlayerActionService _actionService;
		private readonly IWinnerService _winnerService;
		private readonly IConsoleOutputService _consoleOutputService;

		public PlayRoundService(
			IBettingService bettingService, 
			IPlayerParticipationService playerParticipationService, 
			IDealCardService dealCardService, 
			IShowCardService showCardService, 
			IPlayerActionService actionService, 
			IWinnerService winnerService, 
			IConsoleOutputService consoleOutputService)
		{
			_bettingService = bettingService;
			_playerParticipationService = playerParticipationService;
			_dealCardService = dealCardService;
			_showCardService = showCardService;
			_actionService = actionService;
			_winnerService = winnerService;
			_consoleOutputService = consoleOutputService;
		}

		public void PlayRound(List<Player> players, Deck deck, Pot pot, int indexOfDealer, int blindAmount)
		{
			var communityCards = new List<Card>(5);
			
			_consoleOutputService.OutputCurrentStage("Pre Flop");
			if (!PreFlop(players, deck, pot, indexOfDealer, blindAmount, out var participatingPlayers))
			{
				return;
			}
			PlayersAct(participatingPlayers, pot, indexOfDealer);

			var stageCardsDealtMap = new Dictionary<string, int>()
			{
				{"Flop", 3},
				{"Turn", 1},
				{"River", 1},
			};

			foreach (var (stage, cardsToDeal) in stageCardsDealtMap)
			{
				_consoleOutputService.OutputCurrentStage(stage);
				DealCommunityCard(deck, communityCards, cardsToDeal);
				PlayersAct(participatingPlayers, pot, indexOfDealer);
				if (!IsEnoughPlayers(participatingPlayers))
				{
					break;
				}
			}

			var winner = _winnerService.FindWinner(participatingPlayers, communityCards);
			
			_consoleOutputService.OutputPlayerHands(participatingPlayers);
			Console.WriteLine($"{winner} wins with {string.Join(", ", winner.Hand)}");
			winner.WinChips(pot);
			ClearHands(players);
		}

		private bool PreFlop(List<Player> players, Deck deck, Pot pot, int indexOfDealer, int blindAmount,
			out List<Player> participatingPlayers)
		{
			participatingPlayers = _playerParticipationService.GetPlayerParticipation(players, blindAmount);
			if (!IsEnoughPlayers(participatingPlayers))
			{
				Console.WriteLine("Not enough players to start");
				return false;
			}

			var (smallBlind, bigBlind) = _bettingService.DetermineBlindBetPlayers(participatingPlayers, indexOfDealer);
			_bettingService.BlindBets(smallBlind, bigBlind, blindAmount, pot);
			_consoleOutputService.OutputRoles(smallBlind, bigBlind, players[indexOfDealer]);

			_dealCardService.DealCards(participatingPlayers, deck, 2);
			_showCardService.ShowCards(participatingPlayers);
			return true;
		}

		private void DealCommunityCard(Deck deck, List<Card> communityCards, int amount)
		{
			_dealCardService.DealCommunityCards(deck, communityCards, amount);
			Console.WriteLine(string.Join(", ", communityCards));
		}

		private void PlayersAct(List<Player> players, Pot pot, int indexOfFirstPlayer)
		{
			var visited = 0;
			for (int i = indexOfFirstPlayer; visited < players.Count; i = (i + 1) % players.Count)
			{
				_consoleOutputService.OutputPot(pot);
				var actionTaken = PlayerAct(players, players[i], pot);
				FurtherAction(players, players[i], pot, actionTaken);
				visited++;
			}
			
			pot.CurrentBet = 0;
			ResetBets(players);
		}

		private PlayerAction PlayerAct(List<Player> players, Player player, Pot pot)
		{
			var isBetPlaced = pot.CurrentBet != 0;
			var actionTaken = _actionService.Act(player, pot, isBetPlaced);
			return actionTaken;
		}

		private void FurtherAction(List<Player> allPlayers, Player player, Pot pot, PlayerAction actionTaken)
		{
			switch (actionTaken)
			{
				case PlayerAction.Fold:
					allPlayers.Remove(player);
					break;
				case PlayerAction.Raise:
					CollectChipsFromNewBet(allPlayers, pot);
					break;
				case PlayerAction.Bet:
					CollectChipsFromNewBet(allPlayers, pot);
					break;
			}
		}

		private void CollectChipsFromNewBet(List<Player> players, Pot pot)
		{
			foreach (var player in players)
			{
				if (player.CurrentBet != pot.CurrentBet)
				{
					var actionTaken = _actionService.Act(player, pot, true);
					FurtherAction(players, player, pot, actionTaken);
				}
			}
		}

		private void ClearHands(List<Player> players)
		{
			players.ForEach(p => p.ClearHand());
		}

		private void ResetBets(List<Player> players)
		{
			players.ForEach(p => p.CurrentBet = 0);
		}

		private bool IsEnoughPlayers(List<Player> players)
		{
			return players.Count >= 2;
		}
	}
}