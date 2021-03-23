using Infra.Abstractions;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Infra.NH.QueryFilters.Seed;

namespace MyCloset.Infra.NH.QueryFilters
{
	public class TagQueryFilter : EntityQueryFilter<Tag>, IQueryFilter<Tag>, ITagQueryFilter
	{
		public TagQueryFilter(string name) : base(name)
		{

		} 
		public TagQueryFilter()
		{

		}
	}
}
