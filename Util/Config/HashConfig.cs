using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Config
{
	public interface IHashConfig
	{
		string Salt { get; }
	}
	public class HashConfig : IHashConfig
	{
		public string Salt { get; }

		public HashConfig(string salt)
		{
			Salt = salt;
		}
	}
}
