using MyCloset.Domain.Entities;
using MyCloset.Infra.Map.Seed;

namespace MyCloset.Infra.Map.Entities
{
	public class TagMap : EntityMap<Tag>
	{
		public TagMap() : base()
		{
			Table("tag");
		}
	}
}
