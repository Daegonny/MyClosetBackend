using Base.Domain;
using Infra.Abstractions;
using NHibernate;
using Util.Extensions;

namespace MyCloset.Infra.NH.QueryFilters.Seed
{
	public class EntityQueryFilter<T> : IQueryFilter<T>
		where T :  Entity
	{
		public string Name { get; set; }
		public EntityQueryFilter(string name)
		{
			Name = name;
		}
		public EntityQueryFilter()
		{

		}

		public IQueryOver<T, T> ApplyFilters(IQueryOver<T, T> query)
		{
			if (Name.NotNull() && Name.Trim().Length > 0)
				query = query.WhereRestrictionOn(q => q.Name).IsInsensitiveLike($"%{Name}%");

			return query;
		}
	}
}
