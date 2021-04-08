using System.Collections.Generic;
using TexasHoldem.Domain.Entities;

namespace TexasHoldem.ConsoleUI.Services
{
	public interface IShowCardService
	{
		void ShowCards(List<Player> players);
	}
}