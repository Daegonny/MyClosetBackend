using Exceptions.NotFound;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Util.Extensions
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> AssertContainsAll<T>(this IEnumerable<T> enumerable, 
			IEnumerable<long> elements, Func<T, long> idAcessor)
		{
			var searchSpace = enumerable.Select(e => idAcessor(e));
			var notFoundElements = elements.Where(e => !searchSpace.Contains(e));
			if (notFoundElements.Any())
				throw new EntityNotFoundException(notFoundElements);
			return enumerable;
		}

		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
			=> enumerable == null || enumerable.Count() == 0;

		public static Dictionary<K, T> Merge<K, T>(this Dictionary<K, T> baseDictionary,
			IEnumerable<T> newElements, Func<T, K> acessor)
		{
			foreach (var element in newElements)
				if (!baseDictionary.ContainsKey(acessor(element)))
					baseDictionary[acessor(element)] = element;
			return baseDictionary;
		}
	}
}
