using System.Collections.Generic;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Enums;

namespace TexasHoldem.ConsoleUI.Services
{
	public interface IPlayerActionService
	{
		PlayerAction Act(Player player, Pot pot, bool isBetPlaced);
	}
}