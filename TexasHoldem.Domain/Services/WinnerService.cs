using System;
using System.Collections.Generic;
using System.Linq;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Enums;

namespace TexasHoldem.Domain.Services
{
	public class WinnerService : IWinnerService
	{
		private readonly IHandEvaluator _handEvaluator;

		public WinnerService(IHandEvaluator handEvaluator)
		{
			_handEvaluator = handEvaluator;
		}

		public Player FindWinner(List<Player> players, List<Card> communityCards)
		{
			DeterminePlayersBestHands(players, communityCards);

			var winner = players.Aggregate((x, y) => x.BestHand > y.BestHand ? x : y);

			return winner;
		}
		

		private void DeterminePlayersBestHands(List<Player> players, List<Card> communityCards)
		{
			foreach (var player in players)
			{
				player.BestHand = _handEvaluator.BestHand(player.Hand, communityCards);
			}
		}

		private void AddPlayersToDictionary(Dictionary<Player, HandRanking> playerHandMap, List<Player> players)
		{
			foreach (var player in players)
			{
				playerHandMap.Add(player, null);
			}
		}
	}
}