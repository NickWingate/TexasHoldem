using System;
using TexasHoldem.Domain.Enums;

namespace TexasHoldem.Domain.Services
{
	public interface IActionProvider
	{
		Action GetAction(PlayerAction action);
	}
}