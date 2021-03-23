
using Base.Domain;
using NHibernate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Abstractions
{
	public interface IRepository<T, Q> 
		where T : Entity
		where Q : IQueryFilter<T>
	{
		Task<IEnumerable<T>> AllAsync();
		Task<IEnumerable<T>> AllValidAsync();
		Task<T> ByIdAsync(long id);
		Task<IEnumerable<T>> ByIdsAsync(IEnumerable<long> ids);
		Task UpdateAsync(T entity);
		Task<T> SaveAsync(T entity);
		Task<IEnumerable<T>> SaveAsync(IEnumerable<T> entities);
		Task RemoveAsync(long id);
		Task RemoveAsync(T entity);
		Task DisableAsync(long id);
		Task RecoverAsync(long id);
		Task<IEnumerable<T>> PagedAsync(int start, int quantity);
		Task<IEnumerable<T>> PagedAsync(IQueryOver<T, T> query, int start, int quantity);
		Task<IEnumerable<T>> FilteredAsync(Q queryFilter, int start, int quantity);
		Task<int> FilteredRowCountAsync(Q queryFilter);
	}
}
