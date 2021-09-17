using System;
using System.IO;
using Util.Config;

namespace Util.Services
{
	public class ContextTools : IContextTools
	{
		IPathConfig PathConfig { get; }

		public ContextTools(IPathConfig pathConfig)
		{
			PathConfig = pathConfig;
		}
		public DateTime Now() => DateTime.Now;
		public DateTime Today() => DateTime.Today;
		public string DefaultBasePath() => PathConfig.DefaultBasePath;
		public string CombinePaths(string basePath, string subPath) => Path.Join(basePath, subPath);
		public string GetFileName(string s) => s.Substring(0, s.LastIndexOf("."));
		public string GetFileExtension(string s) => s.Split("/")[1];
	}
}
