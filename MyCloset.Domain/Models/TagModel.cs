using Base.Domain;
using MyCloset.Domain.Entities;
using Util.Extensions;

namespace MyCloset.Domain.Models
{
	public class TagModel : EntityModel<Tag>
	{
		public override Tag Update(Tag entity)
		{
			entity.Name = Name;
			return entity;
		}
	}
}
