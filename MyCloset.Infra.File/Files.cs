using MyCloset.Infra.Abstractions.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Util.Services;

namespace MyCloset.Infra.File
{
	public class Files : IFiles
	{
		public Files(IContextTools contextTools)
		{
			ContextTools = contextTools;
		}

		IContextTools ContextTools { get; }

		string FullFilePath(string basePath, string path, string file)
		{
			var combinedPath = ContextTools.CombinePaths(basePath, path);
			return ContextTools.CombinePaths(combinedPath, file);
		}

		string FullFilePath(string basePath, string path, string file, string extension) 
			=> FullFilePath(basePath, path, $"{file}.{extension}");

		public void Remove(string basePath, string path, string file, string extension) 
		{
			var fullFilePath = FullFilePath(basePath, path, file, extension);
			if(System.IO.File.Exists(fullFilePath))
				System.IO.File.Delete(fullFilePath);
		}

		public IEnumerable<string> Save(IEnumerable<HashableFile> hashableFiles) 
			=> hashableFiles.Select(hashableFile => Save(hashableFile));
		
		public string Save(HashableFile hashableFile)
		{
			var hashedPath = ContextTools.CombinePaths(ContextTools.DefaultBasePath(), hashableFile.HashedPath);
			var pathName = ContextTools.CombinePaths(hashedPath, $"{hashableFile.HashedName}.{hashableFile.Extension}");
			using (var fileStream = new FileStream(pathName, FileMode.Create, FileAccess.Write))
				hashableFile.File.CopyTo(fileStream);
			return pathName;
		}

		public byte[] Read(string basePath, string path, string name)
		{
			var fullFilePath = FullFilePath(basePath, path, name);
			try
			{
				return System.IO.File.ReadAllBytes(fullFilePath);
			}
			catch (DirectoryNotFoundException exception)
			{
				throw new Exceptions.NotFound.FileNotFoundException(fullFilePath, exception);
			}
		} 
	}
}
