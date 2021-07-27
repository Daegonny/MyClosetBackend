using Base.Domain;
using Infra.Abstractions;
using MyCloset.Services.Abstractions.Seed;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util.Extensions;
using Util.Services;

namespace MyCloset.Services.Seed
{
	public class CrudService<T, Q> : ICrudService<T, Q>
		where T : Entity
		where Q : IQueryFilter<T>
	{
		protected IRepository<T, Q> Repository { get; }
		IContextTools ContextTools { get; }

		public CrudService
		(
			IRepository<T, Q> repository, 
			IContextTools contextTools
		)
		{
			Repository = repository;
			ContextTools = contextTools;
		}

		public async Task<T> ByIdAsync(long id) 
			=> (await Repository.ByIdAsync(id)).AssertIsNotNull(id);

		public async Task<IEnumerable<T>> ByIdsAsync(IEnumerable<long> ids) 
			=> (await Repository.ByIdsAsync(ids))
			.AssertContainsAll<T>(ids, t => t.Id.GetValueOrDefault());

		public async Task<T> SaveAsync(T entity)  
			=> await Repository.SaveAsync(entity);

		public async Task<IEnumerable<T>> SaveAsync(IEnumerable<T> entities) 
			=> await Repository.SaveAsync(entities);

		public async Task<IEnumerable<T>> FilteredAsync(Q queryFilter, int start, int quantity) 
			=> await Repository.FilteredAsync(queryFilter, start, quantity);

		public async Task<int> FilteredRowCountAsync(Q queryFilter)
			=> await Repository.FilteredRowCountAsync(queryFilter);

		public async Task UpdateAsync(T entity)
		{
			entity.LastUpdate = ContextTools.Now();
			await Repository.UpdateAsync(entity);
		}
	}
}
