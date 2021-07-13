using Base.Domain;
using FluentNHibernate.Mapping;

namespace MyCloset.Infra.Map.Seed
{
	public class EntityMap<T> : ClassMap<T>, IMap
		where T : Entity
	{
		public EntityMap()
		{
			Id(x => x.Id).GeneratedBy.Identity();
			Map(x => x.Name);
			Map(x => x.Creation);
			Map(x => x.LastUpdate).Column("last_update");
			Map(x => x.Exclusion);
		}
	}
}
