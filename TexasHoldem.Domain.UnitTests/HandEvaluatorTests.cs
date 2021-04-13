using System.Collections.Generic;
using FluentAssertions;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Enums;
using TexasHoldem.Domain.Services;
using Xunit;

namespace TexasHoldem.Domain.UnitTests
{
	public class HandEvaluatorTests
	{
		private readonly IHandEvaluator _sut;

		public HandEvaluatorTests()
		{
			_sut = new HandEvaluator();
		}

		[Fact]
		public void ContainsRoyalFlush_ShouldReturnTrue_WhenRoyalFlush()
		{
			// Arrange
			var cards = new List<Card>
			{
				new Card(Suit.Hearts, Rank.Ten),
				new Card(Suit.Hearts, Rank.Jack),
				new Card(Suit.Hearts, Rank.Queen),
				new Card(Suit.Hearts, Rank.King),
				new Card(Suit.Hearts, Rank.Ace),
				new Card(Suit.Hearts, Rank.Seven),
				new Card(Suit.Spades, Rank.Seven),
			};
			// Act
			var actual = _sut.ContainsRoyalFlush(cards);
			// Assert
			actual.Should().BeTrue();
		}
		
		/// <summary>
		/// Clear straight is when there are no cards blocking the straight after sorting
		/// </summary>
		/// <example>
		///	2, 3, 4, 5, 6, K , J is clear
		/// 1, 3, 7, 8, 9, 10, J is not since 1 and 3 precede the straight
		/// </example>
		[Fact]
		public void ContainsStraight_ShouldReturnTrue_WhenClearStraightExists()
		{
			// Arrange
			var cards = new List<Card>
			{
				new Card(Suit.Clubs, Rank.Two),
				new Card(Suit.Clubs, Rank.Three),
				new Card(Suit.Clubs, Rank.Four),
				new Card(Suit.Clubs, Rank.Five),
				new Card(Suit.Clubs, Rank.Six),
				new Card(Suit.Clubs, Rank.King),
				new Card(Suit.Clubs, Rank.Jack),
			};
			// Act
			var actual = _sut.ContainsStraight(cards);

			// Assert
			actual.Should().BeTrue();
		}
		
		[Fact]
		public void ContainsStraight_ShouldReturnTrue_WhenUnorderedStraightExists()
		{
			// Arrange
			var cards = new List<Card>
			{
				new Card(Suit.Clubs, Rank.King),
				new Card(Suit.Clubs, Rank.Three),
				new Card(Suit.Clubs, Rank.Jack),
				new Card(Suit.Clubs, Rank.Six),
				new Card(Suit.Clubs, Rank.Five),
				new Card(Suit.Clubs, Rank.Two),
				new Card(Suit.Clubs, Rank.Four),
			};
			// Act
			var actual = _sut.ContainsStraight(cards);

			// Assert
			actual.Should().BeTrue();
		}

		[Theory]
		[InlineData(1,2,3,4,5,7,8)]
		[InlineData(10,11,12,13,1,6,8)]
		[InlineData(1,2,3,4,5,6,7)]
		public void ContainsStraight_ShouldReturnTrue_WhenStraightExists(
			int a, int b, int c, int d, int e, int f, int g)
		{
			// Arrange
			var cards = new List<Card>
			{
				new Card(Suit.Clubs, a),
				new Card(Suit.Clubs, b),
				new Card(Suit.Clubs, c),
				new Card(Suit.Clubs, d),
				new Card(Suit.Clubs, e),
				new Card(Suit.Clubs, f),
				new Card(Suit.Clubs, g),
			};
			// Act
			var actual = _sut.ContainsStraight(cards);

			// Assert
			actual.Should().BeTrue();
		}
		
		[Theory]
		[InlineData(1,2,3,11,5,6,7)]
		[InlineData(1,2,3,4,8,6,7)]
		[InlineData(9,11,12,13,1,2,3)]
		public void ContainsStraight_ShouldReturnFalse_WhenNoStraightExists(
			int a, int b, int c, int d, int e, int f, int g)
		{
			// Arrange
			var cards = new List<Card>
			{
				new Card(Suit.Clubs, a),
				new Card(Suit.Clubs, b),
				new Card(Suit.Clubs, c),
				new Card(Suit.Clubs, d),
				new Card(Suit.Clubs, e),
				new Card(Suit.Clubs, f),
				new Card(Suit.Clubs, g),
			};
			// Act
			var actual = _sut.ContainsStraight(cards);

			// Assert
			actual.Should().BeFalse();
		}

		[Theory]
		[InlineData(Suit.Clubs, Suit.Clubs, Suit.Clubs, Suit.Clubs, Suit.Clubs, Suit.Clubs, Suit.Clubs)]
		[InlineData(Suit.Hearts, Suit.Spades, Suit.Hearts, Suit.Diamonds, Suit.Hearts, Suit.Hearts, Suit.Hearts)]
		public void ContainsFlush_ShouldReturnTrue_WhenFlushExists(
			Suit a, Suit b, Suit c, Suit d, Suit e, Suit f, Suit g)
		{
			// Arrange
			var cards = new List<Card>
			{
				new Card(a, Rank.Ace),
				new Card(b, Rank.Ace),
				new Card(c, Rank.Ace),
				new Card(d, Rank.Ace),
				new Card(e, Rank.Ace),
				new Card(f, Rank.Ace),
				new Card(g, Rank.Ace),
			};
			// Act
			var actual = _sut.ContainsFlush(cards);
			// Assert
			actual.Should().BeTrue();
		}
		
		[Theory]
		[InlineData(Suit.Spades, Suit.Clubs, Suit.Hearts, Suit.Clubs, Suit.Diamonds, Suit.Clubs, Suit.Clubs)]
		[InlineData(Suit.Hearts, Suit.Spades, Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Hearts, Suit.Hearts)]
		public void ContainsFlush_ShouldReturnFalse_WhenNoFlushExists(
			Suit a, Suit b, Suit c, Suit d, Suit e, Suit f, Suit g)
		{
			// Arrange
			var cards = new List<Card>
			{
				new Card(a, Rank.Ace),
				new Card(b, Rank.Ace),
				new Card(c, Rank.Ace),
				new Card(d, Rank.Ace),
				new Card(e, Rank.Ace),
				new Card(f, Rank.Ace),
				new Card(g, Rank.Ace),
			};
			// Act
			var actual = _sut.ContainsFlush(cards);
			// Assert
			actual.Should().BeFalse();
		}

		[Theory]
		[InlineData(2, 1, 2, 3, 3, 4, 5, 6)]
		[InlineData(2, 1, 1, 3, 3, 4, 5, 6)]
		[InlineData(3, 1, 2, 3, 3, 3, 5, 6)]
		[InlineData(3, 1, 2, 3, 3, 4, 1, 1)]
		[InlineData(4, 3, 3, 3, 3, 4, 1, 1)]
		[InlineData(4, 1, 1, 3, 3, 4, 1, 1)]
		public void ContainsXOfAKind_ShouldReturnTrue_WhenXRanksExist(
			int x, int a, int b, int c, int d, int e, int f, int g)
		{
			// Arrange
			var cards = new List<Card>
			{
				new Card(Suit.Clubs, a),
				new Card(Suit.Clubs, b),
				new Card(Suit.Clubs, c),
				new Card(Suit.Clubs, d),
				new Card(Suit.Clubs, e),
				new Card(Suit.Clubs, f),
				new Card(Suit.Clubs, g),
			};
			// Act
			var actual = _sut.ContainsXOfAKind(x, cards);
			// Assert
			actual.Should().BeTrue();
		}
		
		[Theory]
		[InlineData(2, 1, 2, 3, 4, 5, 6, 7)]
		[InlineData(2, 1, 1, 1, 3, 4, 5, 6)]
		[InlineData(2, 7, 5, 1, 12, 5, 5, 6)]
		[InlineData(3, 1, 2, 3, 3, 4, 5, 6)]
		[InlineData(3, 1, 2, 3, 3, 1, 1, 1)]
		[InlineData(4, 3, 3, 3, 1, 4, 1, 1)]
		public void ContainsXOfAKind_ShouldReturnFalse_WhenNotXRanksExist(
			int x, int a, int b, int c, int d, int e, int f, int g)
		{
			// Arrange
			var cards = new List<Card>
			{
				new Card(Suit.Clubs, a),
				new Card(Suit.Clubs, b),
				new Card(Suit.Clubs, c),
				new Card(Suit.Clubs, d),
				new Card(Suit.Clubs, e),
				new Card(Suit.Clubs, f),
				new Card(Suit.Clubs, g),
			};
			// Act
			var actual = _sut.ContainsXOfAKind(x, cards);
			// Assert
			actual.Should().BeFalse();
		}

		[Theory]
		[InlineData(1,1,3,3,5,6,7)]
		[InlineData(1,1,3,7,5,6,7)]
		public void ContainsTwoPair_ShouldReturnTrue_WhenTwoPairsExists(
			int a, int b, int c, int d, int e, int f, int g)
		{
			// Arrange
			var cards = new List<Card>
			{
				new Card(Suit.Clubs, a),
				new Card(Suit.Clubs, b),
				new Card(Suit.Clubs, c),
				new Card(Suit.Clubs, d),
				new Card(Suit.Clubs, e),
				new Card(Suit.Clubs, f),
				new Card(Suit.Clubs, g),
			};
			// Act
			var actual = _sut.ContainsTwoPair(cards);
			// Assert
			actual.Should().BeTrue();
		}
		
		[Theory]
		[InlineData(1,1,3,4,5,6,7)]
		[InlineData(1,1,1,1,3,3,3)]
		public void ContainsTwoPair_ShouldReturnFalse_WhenNotTwoPairsExists(
			int a, int b, int c, int d, int e, int f, int g)
		{
			// Arrange
			var cards = new List<Card>
			{
				new Card(Suit.Clubs, a),
				new Card(Suit.Clubs, b),
				new Card(Suit.Clubs, c),
				new Card(Suit.Clubs, d),
				new Card(Suit.Clubs, e),
				new Card(Suit.Clubs, f),
				new Card(Suit.Clubs, g),
			};
			// Act
			var actual = _sut.ContainsTwoPair(cards);
			// Assert
			actual.Should().BeFalse();
		}
	}
}