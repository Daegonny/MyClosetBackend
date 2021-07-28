using Base.Domain;
using Infra.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCloset.Services.Abstractions.Seed
{
	public interface ICrudService<T, Q> 
		where T : Entity
		where Q : IQueryFilter<T>
	{
		Task<T> ByIdAsync(long id);
		Task<T> SaveAsync(T entity);
		Task UpdateAsync(T entity);
		Task<IEnumerable<T>> SaveAsync(IEnumerable<T> entities);
		Task<IEnumerable<T>> FilteredAsync(Q queryFilter, int start, int quantity);
		Task<IEnumerable<T>> ByIdsAsync(IEnumerable<long> ids);
		Task<int> FilteredRowCountAsync(Q queryFilter);
	}
}
