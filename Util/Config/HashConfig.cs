
namespace Util.Config
{
	public interface IHashConfig
	{
		string Secret { get; }
	}
	public class HashConfig : IHashConfig
	{
		public string Secret { get; }

		public HashConfig(string secret)
		{
			Secret = secret;
		}
	}
}
