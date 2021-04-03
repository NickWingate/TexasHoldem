using System;

namespace TexasHoldem.Domain.Exceptions
{
	public class EmptyDeckException : Exception
	{
		public EmptyDeckException() : base("The deck is empty")
		{
		}
	}
}