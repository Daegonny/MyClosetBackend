
using Base.Domain;
using NHibernate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Abstractions
{
	public interface IRepository<T> where T : Entity
	{
		Task<T> ByIdAsync(long id);
		Task<IEnumerable<T>> ByIdsAsync(IEnumerable<long> ids);
		Task UpdateAsync(T entity);
		Task<T> SaveAsync(T entity);
		Task<IEnumerable<T>> SaveAsync(IEnumerable<T> entities);
		Task RemoveAsync(T entity);
		
	}
}
