using Base.Domain;
using Exceptions.BadRequest;
using MyCloset.Domain.Entities;
using Resources;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Util.Extensions;

namespace MyCloset.Domain.Models
{
	public class PieceModel : EntityModel<Piece>
	{
		public decimal? Price { get; set; }
		public string Purchase { get; set; }
		public IEnumerable<string> TagNames { get; set; }

		[JsonIgnore]
		public List<Tag> Tags { get; private set; }

		public Piece Update(Piece entity)
		{
			entity.Name = Name;
			entity.Price = Price;
			entity.Purchase = Purchase.ToDate();
			entity.Tags = Tags;
			return entity;
		}

		public PieceModel FillTags(Dictionary<string, Tag> tagsDictionary) 
		{
			Tags = new List<Tag>();
			var tagKeys = TagNames.Select(t => t.Clean());
			foreach (var tagKey in tagKeys)
				Tags.Add(tagsDictionary[tagKey]);
			
			return this;
		}

		public void Validate()
		{
			if (Price < 0)
				throw new BadRequestException(string.Format(Resource.MinSizeError, Resource.NameField, 0));

			if (TagNames.Count() > 10)
				throw new BadRequestException(string.Format(Resource.OverLimitError, Resource.TagsField, 10));
		}
	}
}
