using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using TexasHoldem.Domain.Entities;
using TexasHoldem.WpfUI.Services.Interfaces;

namespace TexasHoldem.WpfUI.Services
{
	public class PlayRoundService : IPlayRoundService
	{
		public Player PlayRound(List<Player> players, Deck deck, Pot pot, ICollection<Card> communityCards,
			int indexOfDealer, int blindAmount)
		{
			for (int i = 0; i < 3; i++)
			{
				communityCards.Add(deck.DrawCard());
				Thread.Sleep(100);
			}
			// PlayersAct(players, pot, indexOfDealer);
			// PostFlopRounds(deck, pot, indexOfDealer, communityCards, players);
			//
			// var winner = _winnerService.FindWinner(participatingPlayers, communityCards);
			//
			// winner.WinChips(pot);
			// ClearHands(players);
			//
			// return winner;

			return default;
		}
	}
}