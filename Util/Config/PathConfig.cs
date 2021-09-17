
namespace Util.Config
{
	public interface IPathConfig
	{
		string DefaultUserPath { get; }
		string DefaultBasePath { get; }
	}
	public class PathConfig : IPathConfig
	{
		public PathConfig(string defaultUserPath, string defaultBasePath)
		{
			DefaultUserPath = defaultUserPath;
			DefaultBasePath = defaultBasePath;
		}

		public string DefaultUserPath { get; }
		public string DefaultBasePath { get; }
	}
}
