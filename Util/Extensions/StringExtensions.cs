using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Util.Extensions
{
	public static class StringExtensions
	{
		public static Dictionary<char, char> CharCleaner = new Dictionary<char, char>() {
			{ 'a','a' },
			{ 'á','a' },
			{ 'à','a' },
			{ 'â','a' },
			{ 'ã','a' },
			{ 'ä','a' },
			{ 'b','b' },
			{ 'c','c' },
			{ 'ĉ','c' },
			{ 'ç','c' },
			{ 'd','d' },
			{ 'e','e' },
			{ 'é','e' },
			{ 'è','e' },
			{ 'ê','e' },
			{ 'ë','e' },
			{ 'f','f' },
			{ 'g','g' },
			{ 'ĝ','g' },
			{ 'h','h' },
			{ 'ĥ','h' },
			{ 'i','i' },
			{ 'í','i' },
			{ 'ì','i' },
			{ 'î','i' },
			{ 'ï','i' },
			{ 'j','j' },
			{ 'ĵ','j' },
			{ 'k','k' },
			{ 'l','l' },
			{ 'm','m' },
			{ 'n','n' },
			{ 'ñ','n' },
			{ 'o','o' },
			{ 'ó','o' },
			{ 'ò','o' },
			{ 'ô','o' },
			{ 'õ','o' },
			{ 'ö','o' },
			{ 'p','p' },
			{ 'q','q' },
			{ 'r','r' },
			{ 's','s' },
			{ 'ŝ','s' },
			{ 't','t' },
			{ 'u','u' },
			{ 'ú','u' },
			{ 'ù','u' },
			{ 'û','u' },
			{ 'ü','u' },
			{ 'v','v' },
			{ 'w','w' },
			{ 'x','x' },
			{ 'y','y' },
			{ 'z','z' },
			{ '0','0' },
			{ '1','1' },
			{ '2','2' },
			{ '3','3' },
			{ '4','4' },
			{ '5','5' },
			{ '6','6' },
			{ '7','7' },
			{ '8','8' },
			{ '9','9' },
			{ '-','-' },
			{ '_','_' }
		};
		public static T Deserialize<T>(this string json)
			=> JsonConvert.DeserializeObject<T>(json);

		public static string Hash(this string input)
		{
			byte[] data = SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
			var sBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
				sBuilder.Append(data[i].ToString("x2"));
			return sBuilder.ToString();
		}

		public static string Encrypt(this string input, DateTime? salt, string secret) 
			=> $"{input}{(salt.HasValue ? salt.ToString() : string.Empty)}{secret}".Hash();

		public static decimal? ToDecimal(this string input)
		{
			if (input.IsNullOrEmpty())
				return null;
			else
			{
				return decimal.Parse(input.Replace(',','.'), new CultureInfo("en-US"));
			}
		}

		public static DateTime ToDate(this string input)
		{
			var splited = input.Split('-');
			return new DateTime(Int32.Parse(splited[0]), Int32.Parse(splited[1]), Int32.Parse(splited[2]));
		}

		public static string Clean(this string input)
		{
			var output = new StringBuilder();
			input = input.ToLower();
			foreach(var character in input)
				if (CharCleaner.ContainsKey(character))
					output.Append(CharCleaner[character]);
			return output.ToString();
		}

		public static bool IsNullOrEmpty(this string input) 
			=> input.IsNull() || input.Trim().Length == 0;
	}
}
