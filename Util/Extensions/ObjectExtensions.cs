using Exceptions;
using Newtonsoft.Json;

namespace Util.Extensions
{
	public static class ObjectExtensions
	{
		public static T AssertIsNotNull<T>(this T obj, long id)
		{
			if (obj.IsNull())
				throw new NotFoundException(id);
			return obj;
		}

		public static bool IsNull(this object obj)
		{
			return obj == null;
		}

		public static bool NotNull(this object obj)
		{
			return !obj.IsNull();
		}
		public static bool IsDecimal(this object value)
		{
			return value is float
					|| value is double
					|| value is decimal;
		}

		public static string Serialize<T>(this T obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
	}
}
