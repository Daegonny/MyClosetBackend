using System;

namespace Util.Services
{
	public interface IContextTools
	{
		DateTime Now();
		string DefaultBasePath();
		string FileExtension();
		string GetFileName(string s);
		string GetFileExtension(string s);
		string CombinePaths(string basePath, string subPath);
		string DefaultUserPath();
		string DefaultFileType();
		DateTime Today();
	}
}
