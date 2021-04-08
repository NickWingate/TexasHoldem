using System;
using System.Collections.Generic;
using TexasHoldem.Domain.Enums;
using TexasHoldem.Domain.Exceptions;

namespace TexasHoldem.Domain.Entities
{
	public class Deck
	{
		public Deck()
		{
			InitializeDeck();
		}

		public List<Card> Cards { get; private set; } = new List<Card>();
		public int Count => Cards.Count;
		public bool IsEmpty => Cards.Count == 0;
        
		public void Shuffle()
		{
			var rng = new Random();
			for (var n = Cards.Count - 1; n > 0; n--)
			{
				var r = rng.Next(n + 1);
				var temp = Cards[n];
				Cards[n] = Cards[r];
				Cards[r] = temp;
			}
		}
        
		public Card DrawCard()
		{
			if (Cards.Count > 0)
			{
				var tempCard = Cards[0];
				Cards.RemoveAt(0);
				return tempCard;
			}
			throw new EmptyDeckException();
		}
		
		private void InitializeDeck()
		{
			foreach (Suit s in Enum.GetValues(typeof(Suit)))
			{
				foreach (Rank v in Enum.GetValues(typeof(Rank)))
				{
					Cards.Add(new Card(s, v));
				}
			}
		}
	}
}