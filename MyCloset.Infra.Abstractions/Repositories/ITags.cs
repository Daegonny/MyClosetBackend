using Infra.Abstractions;
using MyCloset.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCloset.Infra.Abstractions.Repositories
{
	public interface ITags : IRepository<Tag>
	{
		Task<IEnumerable<Tag>> ByNamesAsync(IEnumerable<string> tagNames);
	}
}
