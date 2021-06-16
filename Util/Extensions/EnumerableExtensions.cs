using System;
using System.Collections.Generic;
using System.Linq;

namespace Util.Extensions
{
	public static class EnumerableExtensions
	{
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
			=> enumerable == null || enumerable.Count() == 0;

		public static Dictionary<K, T> Merge<K, T>(this Dictionary<K, T> baseDictionary, IEnumerable<T> newElements, Func<T, K> acessor)
		{
			foreach (var element in newElements)
				if (!baseDictionary.ContainsKey(acessor(element)))
					baseDictionary[acessor(element)] = element;
			return baseDictionary;
		}
	}
}
