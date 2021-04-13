using System.Collections.Generic;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Enums;

namespace TexasHoldem.Domain.Services
{
	public class AvailableActionProvider : IAvailableActionProvider
	{
		public PlayerAction[] DetermineAvailableActions(Player player, Pot pot)
		{
			var availableActions = new List<PlayerAction>
			{
				PlayerAction.Fold
			};
			var isInDebt = player.CurrentBet < pot.CurrentBet;
			var isBetPlaced = pot.CurrentBet > 0;
			if (!isInDebt)
			{
				availableActions.Add(PlayerAction.Check);
				if (CanPlayerBet(player) && !isBetPlaced)
				{
					availableActions.Add(PlayerAction.Bet);
				}
				
			}
			else if (CanPlayerCall(player, pot))
			{
				availableActions.Add(PlayerAction.Call);
			}
			if (CanPlayerRaise(player, pot) && isBetPlaced)
			{
				availableActions.Add(PlayerAction.Raise);
			}
			
			return availableActions.ToArray();
		}

		private bool CanPlayerBet(Player player)
		{
			return player.ChipCount > 0;
		}

		private bool CanPlayerCall(Player player, Pot pot)
		{
			return player.ChipCount >= pot.CurrentBet;
		}

		private bool CanPlayerRaise(Player player, Pot pot)
		{
			return player.ChipCount > pot.CurrentBet;
		}
	}
}