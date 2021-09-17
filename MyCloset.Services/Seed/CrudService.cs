using Base.Domain;
using Infra.Abstractions;
using MyCloset.Services.Abstractions.Seed;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util.Extensions;
using Util.Services;

namespace MyCloset.Services.Seed
{
	public class CrudService<T> : ICrudService<T> where T : Entity
	{
		protected IRepository<T> Repository { get; }
		IContextTools ContextTools { get; }

		public CrudService
		(
			IRepository<T> repository, 
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

		public async Task UpdateAsync(T entity)
		{
			entity.LastUpdate = ContextTools.Now();
			await Repository.UpdateAsync(entity);
		}
	}
}
