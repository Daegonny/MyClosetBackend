using MyCloset.Infra.File;
using System.Collections.Generic;
using System.Net.Http;

namespace MyCloset.Infra.Abstractions.Repositories
{
	public interface IFiles
	{
		string Save(HashableFile hashableFile);
		IEnumerable<string> Save(IEnumerable<HashableFile> hashableFiles);
		void Remove(string basePath, string path, string file, string extension);
		byte[] Read(string basePath, string path, string name);
	}
}
