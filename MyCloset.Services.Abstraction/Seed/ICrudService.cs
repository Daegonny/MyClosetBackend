using Base.Domain;
using Infra.Abstractions;
using NHibernate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCloset.Services.Abstractions.Seed
{
	public interface ICrudService<T, Q, M> 
		where T : Entity
		where Q : IQueryFilter<T>
		where M : EntityModel<T>
	{
		Task<IEnumerable<T>> AllAsync();
		Task<IEnumerable<T>> AllValidAsync();
		Task<T> ByIdAsync(long id);
		Task UpdateAsync(T entity);
		Task UpdateAsync(M model);
		Task UpdateAsync(IEnumerable<M> model);
		Task<T> SaveAsync(T entity);
		Task<IEnumerable<T>> SaveAsync(IEnumerable<T> entities);
		Task RemoveAsync(long id);
		Task RemoveAsync(T entity);
		Task DisableAsync(long id);
		Task RecoverAsync(long id);
		Task<IEnumerable<T>> PagedAsync(int start, int quantity);
		Task<IEnumerable<T>> PagedAsync(IQueryOver<T, T> query, int start, int quantity);
		Task<IEnumerable<T>> FilteredAsync(Q queryFilter, int start, int quantity);
		Task<IEnumerable<T>> ByIdsAsync(IEnumerable<long> ids);
		Task<int> FilteredRowCountAsync(Q queryFilter);
	}
}
