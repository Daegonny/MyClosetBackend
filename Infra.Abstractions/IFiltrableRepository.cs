using Base.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Abstractions
{
	public interface IFiltrableRepository<T, Q>
		where T : Entity
		where Q : IQueryFilter<T>
	{
		Task<IEnumerable<T>> FilteredAsync(Q queryFilter, int start, int quantity);
		Task<int> FilteredRowCountAsync(Q queryFilter);
	}
}
