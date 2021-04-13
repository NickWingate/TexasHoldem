using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem.Domain.Extensions
{
	public static class DictionaryExtensions
	{
		/// <summary>
		/// Checks if a dictionary contains any value in the specified range
		/// </summary>
		/// <param name="dictionary"></param>
		/// <param name="lowerBound">inclusive lower bound</param>
		/// <param name="upperBound">exclusive upper bound</param>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <returns></returns>
		public static bool ContainsAnyValueInRange<TKey>(
			this Dictionary<TKey, int> dictionary, int lowerBound, int upperBound)
		{
			var possibleValues = Enumerable.Range(lowerBound, upperBound - lowerBound);
			return possibleValues.Any(dictionary.ContainsValue);
		}
		
	}
}