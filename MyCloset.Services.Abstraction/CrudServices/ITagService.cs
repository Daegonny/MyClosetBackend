using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Services.Abstractions.Seed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCloset.Services.Abstractions.CrudServices
{
	public interface ITagService : ICrudService<Tag, ITagQueryFilter>
	{
		Task<Dictionary<string, Tag>> SaveUniqueAsync(IEnumerable<string> tagNames);
	}
}
