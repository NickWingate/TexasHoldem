using FluentAssertions;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Enums;
using TexasHoldem.WpfUI.Services;
using TexasHoldem.WpfUI.Services.Interfaces;
using Xunit;

namespace TexasHoldem.WpfServices.UnitTests
{
	public class CardCodeServiceTests
	{
		private readonly ICardCodeService _sut;

		public CardCodeServiceTests()
		{
			_sut = new CardCodeService();
		}

		[Fact]
		public void GetCode_ShouldReturnCardCode_WhenEasyCard()
		{
			// Arrange
			var card = new Card(Suit.Diamonds, Rank.Four);
			var expected = "4D";
			// Act
			var cardCode = _sut.GetCode(card);

			// Assert
			cardCode.Should().BeEquivalentTo(expected);
		}
		
		[Fact]
		public void GetCode_ShouldReturnCardCode_WhenComplexCard()
		{
			// Arrange
			var card = new Card(Suit.Diamonds, Rank.Jack);
			var expected = "JD";
			// Act
			var cardCode = _sut.GetCode(card);

			// Assert
			cardCode.Should().BeEquivalentTo(expected);
		}
	}
}