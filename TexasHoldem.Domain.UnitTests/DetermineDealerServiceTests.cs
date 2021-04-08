using System.Collections.Generic;
using TexasHoldem.Domain.Entities;
using TexasHoldem.Domain.Enums;
using TexasHoldem.Domain.Services;
using Xunit;

namespace TexasHoldem.Domain.UnitTests
{
	public class DetermineDealerServiceTests
	{
		private readonly IDetermineDealerService _sut;

		public DetermineDealerServiceTests()
		{
			_sut = new DetermineDealerService();
		}

		[Theory]
		[MemberData(nameof(DifferingSuits_TestData))]
		public void IndexOfDealer_ShouldReturnHigherRankCard_WhenSuitsDiffer(Card card1, Card card2, int expected)
		{
			// Arrange
			var players = new List<Player>
			{
				new() {Hand = new List<Card> {card1}},
				new() {Hand = new List<Card> {card2}}
			};
			// Act
			var actual = _sut.IndexOfDealer(players);

			// Assert
			Assert.Equal(expected, actual);

		}
		
		[Theory]
		[MemberData(nameof(SameRank_TestData))]
		public void IndexOfDealer_ShouldReturnHigherSuitCard_WhenRankIsSame(Card card1, Card card2, int expected)
		{
			// Arrange
			var players = new List<Player>
			{
				new() {Hand = new List<Card> {card1}},
				new() {Hand = new List<Card> {card2}}
			};
			// Act
			var actual = _sut.IndexOfDealer(players);

			// Assert
			Assert.Equal(expected, actual);

		}

		public static IEnumerable<object[]> DifferingSuits_TestData()
		{
			yield return new object[] {new Card(Suit.Clubs, 1), new Card(Suit.Diamonds, 3), 0};
			yield return new object[] {new Card(Suit.Clubs, 13), new Card(Suit.Spades, 10), 0};
			yield return new object[] {new Card(Suit.Clubs, 13), new Card(Suit.Hearts, 1), 1};
			yield return new object[] {new Card(Suit.Hearts, 5), new Card(Suit.Spades, 7), 1};
			yield return new object[] {new Card(Suit.Hearts, 7), new Card(Suit.Spades, 5), 0};
		}

		public static IEnumerable<object[]> SameRank_TestData()
		{
			yield return new object[] {new Card(Suit.Spades, 1), new Card(Suit.Diamonds, 1), 0};
			yield return new object[] {new Card(Suit.Clubs, 1), new Card(Suit.Hearts, 1), 1};
			yield return new object[] {new Card(Suit.Clubs, 1), new Card(Suit.Spades, 1), 1};
			yield return new object[] {new Card(Suit.Diamonds, 1), new Card(Suit.Clubs, 1), 0};
		}
	}
}