using System;
using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.Domain.Services
{
	public class BettingService : IBettingService
	{
		//https://en.wikipedia.org/wiki/Blind_(poker)#:~:text=The%20blinds%20are%20forced%20bets,range%20from%20none%20to%20three.
		public void BlindBets(Player smallBlind, Player bigBlind, int blindAmount, Pot pot)
		{
			var smallBlindPrice = DetermineSmallBlindPrice(blindAmount);
			smallBlind.AddToPot(smallBlindPrice, pot);
			bigBlind.AddToPot(blindAmount, pot);
			
		}

		public (Player, Player) DetermineBlindBetPlayers(List<Player> players, int indexOfDealer)
		{
			var smallBlind = CircularIncrement(players, indexOfDealer, 1);
			var bigBlind = CircularIncrement(players, indexOfDealer, 2);

			if (players.Count == 2)
			{
				smallBlind = players[indexOfDealer];
				bigBlind = CircularIncrement(players, indexOfDealer, 1);
			}
			return (smallBlind, bigBlind);
		}

		public int DetermineSmallBlindPrice(int bigBlind)
		{
			return (int) Math.Round(bigBlind / 2d);
		}
		/// <summary>
		/// Increments in a list, but if the last item is incremented it returns the first item.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="index"></param>
		/// <param name="amount"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		private static T CircularIncrement<T>(IList<T> list, int index, int amount)
		{
			return list[(index + amount) % list.Count];
		}
	}
}