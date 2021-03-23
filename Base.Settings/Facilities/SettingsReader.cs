using System.IO;
using Util.Extensions;

namespace Base.Settings.Facilities
{
	public static class SettingsReader
	{
		public static SettingsModel Get()
		{
			#if DEBUG

			var file = File.Exists(DebugLocal) ? DebugLocal : DebugGlobal;
			return Get(file);

			#else

			var file = File.Exists("appsettings.Local.json") ? "appsettings.Local.json" : "appsettings.json";
			return Get(file);

			#endif

		}
		private static SettingsModel Get(string filePath) => File.ReadAllText(filePath).Deserialize<SettingsModel>();
		private static string DebugBuildFolder { get; } = "./";
		private static string DebugLocal => $"{DebugBuildFolder}/appsettings.Local.json";
		private static string DebugGlobal => $"{DebugBuildFolder}/appsettings.json";
	}
}
