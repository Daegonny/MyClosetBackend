using System.Drawing;

namespace Util.Extensions
{
	public static class ImageExtensions
	{
		public static bool IsOverPixelLimit(this Image image, int limit)
			=> image.Width > limit || image.Height > limit;
	}
}
