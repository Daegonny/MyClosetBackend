using System;

namespace Util.Services
{
	public interface IContextTools
	{
		DateTime Now();
		string DefaultBasePath();
		string GetFileName(string s);
		string GetFileExtension(string s);
		string CombinePaths(string basePath, string subPath);
		string DefaultFileType();
		DateTime Today();
	}
}
