using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Enums;

namespace TexasHoldem.Domain.Services
{
	public interface IAvailableActionProvider
	{
		PlayerAction[] DetermineAvailableActions(Player player, Pot pot);
	}
}