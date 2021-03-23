using System.Collections.Generic;
using System.Linq;

namespace Util.Extensions
{
	public static class EnumerableExtensions
	{
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
			=> enumerable == null || enumerable.Count() == 0;
	}
}
