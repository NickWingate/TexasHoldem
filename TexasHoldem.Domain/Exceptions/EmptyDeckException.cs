using System;
using System.Diagnostics.CodeAnalysis;

namespace TexasHoldem.Domain.Exceptions
{
	public class EmptyDeckException : Exception
	{
		public EmptyDeckException(string message = "No cards left in deck") : base(message)
		{
		}
	}
}