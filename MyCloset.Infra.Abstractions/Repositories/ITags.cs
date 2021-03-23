using Infra.Abstractions;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCloset.Infra.Abstractions.Repositories
{
	public interface ITags : IRepository<Tag, ITagQueryFilter>
	{
		Task<IEnumerable<Tag>> ByNamesAsync(IEnumerable<string> tagNames);
	}
}
