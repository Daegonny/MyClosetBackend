using Base.Domain;
using Infra.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCloset.Services.Abstractions.Seed
{
	public interface ICrudService<T>  where T : Entity
	{
		Task<T> ByIdAsync(long id);
		Task<T> SaveAsync(T entity);
		Task UpdateAsync(T entity);
		Task<IEnumerable<T>> SaveAsync(IEnumerable<T> entities);
	}
}
