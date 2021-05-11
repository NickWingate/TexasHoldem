using TexasHoldem.Domain.Entities;

namespace TexasHoldem.WpfUI.Services.Interfaces
{
	public interface ICardCodeService
	{
		string GetCode(Card card);
	}
}