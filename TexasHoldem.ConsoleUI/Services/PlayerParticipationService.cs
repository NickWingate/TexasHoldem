﻿using System;
using System.Collections.Generic;
using Spectre.Console;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Services;

namespace TexasHoldem.ConsoleUI.Services
{
	public class PlayerParticipationService : IPlayerParticipationService
	{
		private readonly IBettingService _bettingService;

		public PlayerParticipationService(IBettingService bettingService)
		{
			_bettingService = bettingService;
		}

		public List<Player> GetPlayerParticipation(List<Player> players, int blindPrice)
		{
			var smallBlindPrice = _bettingService.DetermineSmallBlindPrice(blindPrice);
			var participatingPlayers = new List<Player>();
			
			Console.WriteLine($"The Big Blind for this round is {blindPrice} chips\n" +
			                  $" and the Small Blind is {smallBlindPrice}");
			
			foreach (var player in players)
			{
				if (AnsiConsole.Confirm($"{player.Name} do you want to participate"))
				{
					participatingPlayers.Add(player);
				}
			}

			return participatingPlayers;
		}
	}
}