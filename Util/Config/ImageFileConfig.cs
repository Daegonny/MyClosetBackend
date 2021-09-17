using System.Collections.Generic;

namespace Util.Config
{
	public interface IImageFileConfig
	{
		int PixeLimit { get; }
		string Extension { get; }
		string ContentType { get; }
	}
	public class ImageFileConfig : IImageFileConfig
	{
		public int PixeLimit { get; }
		public string Extension { get; }
		public string ContentType { get; }

		public ImageFileConfig(int pixeLimit, string extension, string contentType)
		{
			PixeLimit = pixeLimit;
			Extension = extension;
			ContentType = contentType;
		}
	}
}
