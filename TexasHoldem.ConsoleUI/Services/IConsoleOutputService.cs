using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.ConsoleUI.Services
{
	public interface IConsoleOutputService
	{
		void OutputChips(List<Player> players);
		void OutputCurrentStage(string stage);
		void OutputPlayerHands(List<Player> players);
		void OutputRoles(Player smallBlind, Player bigBlind, Player dealer);
		void OutputPot(Pot pot);
	}
}