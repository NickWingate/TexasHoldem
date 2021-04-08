using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.ConsoleUI.Services
{
	public interface IPlayerParticipationService
	{
		List<Player> GetPlayerParticipation(List<Player> players, int blindPrice);
	}
}