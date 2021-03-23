using Microsoft.AspNetCore.Http;

namespace MyCloset.Infra.File
{
	public class HashableFile //TODO: MOVER PARA MODELS
	{
		public IFormFile File;
		public string HashedName;
		public string HashedPath;
		public string Extension;
		public HashableFile()
		{
				
		}
		public HashableFile(IFormFile file, string hashedName, string hashedPath, string extension)
		{
			File = file;
			HashedName = hashedName;
			HashedPath = hashedPath;
			Extension = extension;
		}
	}
}
