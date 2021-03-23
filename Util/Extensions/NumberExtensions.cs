namespace Util.Extensions
{
	public static class NumberExtensions
	{
		public static bool IsNullOrZero(this long? number)
			=> !number.HasValue || (number.HasValue && number.Value == 0);
	}
}
