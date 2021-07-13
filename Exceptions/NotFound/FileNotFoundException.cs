
using Resources;
using System;

namespace Exceptions.NotFound
{
	public class FileNotFoundException : NotFoundException
	{
		public FileNotFoundException(string fileName, Exception inner) 
			: base(string.Format(Resource.FileNotFound, fileName), inner) {}
	}
}
