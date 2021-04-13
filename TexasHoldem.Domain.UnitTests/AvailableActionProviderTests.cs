using System.Security;
using FluentAssertions;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Enums;
using TexasHoldem.Domain.Services;
using Xunit;

namespace TexasHoldem.Domain.UnitTests
{
	public class AvailableActionProviderTests
	{
		private readonly IAvailableActionProvider _sut;

		public AvailableActionProviderTests()
		{
			_sut = new AvailableActionProvider();
		}

		[Fact]
		public void DetermineAvailableActions_ShouldReturnCallFoldRaise_WhenFirstRoundFirstPlayerHasEnoughChips()
		{
			// Arrange
			var expected = new[]
			{
				PlayerAction.Call,
				PlayerAction.Fold,
				PlayerAction.Raise
			};
			var player = new Player()
			{
				ChipCount = 2
			};
			var pot = new Pot();
			pot.Bet(1);
			// Act
			var actual = _sut.DetermineAvailableActions(player, pot);
			// Assert
			actual.Should().BeEquivalentTo(expected);
		}
		
		[Fact]
		public void DetermineAvailableActions_ShouldReturnCheckBetFold_WhenNotFirstRoundButFirstPlayer()
		{
			// Arrange
			var expected = new[]
			{
				PlayerAction.Check,
				PlayerAction.Fold,
				PlayerAction.Bet
			};
			var player = new Player()
			{
				ChipCount = 2
			};
			var pot = new Pot();
			// Act
			var actual = _sut.DetermineAvailableActions(player, pot);
			// Assert
			actual.Should().BeEquivalentTo(expected);
		}
		
		[Fact]
		public void DetermineAvailableActions_ShouldReturnFold_WhenPlayerHasInsufficientChips()
		{
			// Arrange
			var expected = new[]
			{
				PlayerAction.Fold,
			};
			var player = new Player()
			{
				ChipCount = 0
			};
			var pot = new Pot();
			pot.Bet(1);
			// Act
			var actual = _sut.DetermineAvailableActions(player, pot);
			// Assert
			actual.Should().BeEquivalentTo(expected);
		}
	}
}