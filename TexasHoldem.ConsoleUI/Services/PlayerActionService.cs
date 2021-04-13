using System;
using System.Collections.Generic;
using Spectre.Console;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Enums;
using TexasHoldem.Domain.Services;

namespace TexasHoldem.ConsoleUI.Services
{
	public class PlayerActionService : IPlayerActionService
	{
		private readonly IAvailableActionProvider _actionProvider;

		public PlayerActionService(IAvailableActionProvider actionProvider)
		{
			_actionProvider = actionProvider;
		}

		public PlayerAction Act(Player player, Pot pot, bool isBetPlaced)
		{
			var availableActions = _actionProvider.DetermineAvailableActions(player, pot);
			var choice = GetPlayerAction(availableActions, player);
			
			PerformAction(choice, player, pot);

			return choice;
		}

		private void PerformAction(PlayerAction choice, Player player, Pot pot)
		{
			switch (choice)
			{
				case PlayerAction.Check:
					break;
				case PlayerAction.Bet:
					Bet(player, pot);
					break;
				case PlayerAction.Call:
					Call(player, pot);
					break;
				case PlayerAction.Raise:
					Raise(player, pot);
					break;
				case PlayerAction.Fold:
					Fold(player);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(choice), choice, null);
			}
		}

		private void Bet(Player player, Pot pot)
		{
			var betAmount = GetChips(player, "Bet");
			player.AddToPot(betAmount, pot);
		}

		private void Call(Player player, Pot pot)
		{
			player.AddToPot(pot.CurrentBet, pot);
		}

		private void Raise(Player player, Pot pot)
		{
			
			var raiseAmount = GetChips(player, "Raise to");
			player.AddToPot(raiseAmount, pot);
		}

		private void Fold(Player player)
		{
			AnsiConsole.WriteLine($"{player.Name} folds and is no longer in this round");
		}
		
		

		private static int GetChips(Player player, string title, List<Func<int, ValidationResult>> validators = null)
		{
			return AnsiConsole.Prompt(new TextPrompt<int>($"{title} amount in chips:")
				.Validate(bet =>
				{
					if (bet > player.ChipCount)
					{
						return ValidationResult.Error($"[red] You only have {player.ChipCount} chips[/]");
					}

					if (bet < 0)
					{
						return ValidationResult.Error("Your bet cannot be 0");
					}

					if (validators is null) return ValidationResult.Success();
					
					foreach (var validator in validators)
					{
						var result = validator(bet);
						if (result == ValidationResult.Error())
						{
							return result;
						}
					}

					return ValidationResult.Success();
				}));
		}

		private static PlayerAction GetPlayerAction(PlayerAction[] choices, Player player)
		{
			return AnsiConsole.Prompt(new SelectionPrompt<PlayerAction>()
				.Title($"[bold blue]Action for {player}[/]")
				.AddChoices(choices)
				.HighlightStyle(new Style(Color.Green, decoration: Decoration.Bold))
			);
		}
	}
}