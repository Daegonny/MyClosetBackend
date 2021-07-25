using Base.Domain;
using System;
using System.Text.Json.Serialization;
using Util.Extensions;

namespace MyCloset.Domain.Entities
{
	public class Piece : TaggeableEntity<Tag>, IHaveOwner
	{
		public Piece(){}

		[JsonIgnore]
		public virtual string FileName { get; set; }
		[JsonIgnore]
		public virtual string Extension { get; set; }
		[JsonIgnore]
		public virtual string HashedFileName { get; set; }
		[JsonIgnore]
		public virtual string HashedFilePath { get; set; }
		[JsonIgnore]
		public virtual Account Account { get; set; }
		[JsonIgnore]
		public virtual decimal? Price { get; set; }
		public virtual DateTime Purchase { get; set; }
		public virtual string FullFilePath {get => $"{HashedFilePath}/{HashedFileName}.{Extension}";}

		//public virtual IEnumerable<Look> Looks { get; set; }

		public virtual Piece Fill(string fileName, string hashedFileName, string extension, string hashedPath, DateTime purchase)
		{
			FileName = fileName;
			HashedFileName = hashedFileName;
			Extension = extension;
			HashedFilePath = hashedPath;
			Purchase = purchase;
			return this;
		}
	}
}
