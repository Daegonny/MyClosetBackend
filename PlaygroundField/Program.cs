using System;
using Util.Extensions;

namespace PlaygroundField
{
	class Program
	{
		static void Main(string[] args)
		{
			var text = "esse é o meu TEXTô125";
			Console.WriteLine(text);
			Console.WriteLine(text.Clean());
		}
	}
}
