using Base.Domain;
using Infra.Abstractions;
using MyCloset.Services.Abstractions.Seed;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Extensions;
using Util.Services;

namespace MyCloset.Services.Seed
{
	public class CrudService<T, Q, M> : ICrudService<T, Q, M>
		where T : Entity
		where Q : IQueryFilter<T>
		where M : EntityModel<T>
	{
		public CrudService(IRepository<T, Q> repository, IContextTools contextTools)
		{
			Repository = repository;
			ContextTools = contextTools;
		}

		IRepository<T, Q> Repository { get; }
		IContextTools ContextTools { get; }

		public async Task<IEnumerable<T>> AllAsync() => await Repository.AllAsync();

		public async Task<IEnumerable<T>> AllValidAsync() => await Repository.AllValidAsync();

		public async Task<T> ByIdAsync(long id) 
			=> await Repository.ByIdAsync(id).AssertIsNotNull(id);

		public async Task<IEnumerable<T>> ByIdsAsync(IEnumerable<long> ids) => await Repository.ByIdsAsync(ids);

		public async Task DisableAsync(long id) => await Repository.DisableAsync(id);

		public async Task RecoverAsync(long id) => await Repository.RecoverAsync(id);

		public async Task RemoveAsync(long id) => await Repository.RemoveAsync(id);
		public async Task RemoveAsync(T entity) => await Repository.RemoveAsync(entity);

		public async Task<T> SaveAsync(T entity) => await Repository.SaveAsync(entity);

		public async Task<IEnumerable<T>> SaveAsync(IEnumerable<T> entities) => await Repository.SaveAsync(entities);

		public async Task UpdateAsync(T entity) => await Repository.UpdateAsync(entity);

		public async Task<IEnumerable<T>> PagedAsync(int start, int quantity) => await Repository.PagedAsync(start, quantity);
		
		public async Task<IEnumerable<T>> PagedAsync(IQueryOver<T, T> query, int start, int quantity) 
			=> await Repository.PagedAsync(query, start, quantity);

		public async Task<IEnumerable<T>> FilteredAsync(Q queryFilter, int start, int quantity) 
			=> await Repository.FilteredAsync(queryFilter, start, quantity);

		public async Task<int> FilteredRowCountAsync(Q queryFilter)
			=> await Repository.FilteredRowCountAsync(queryFilter);

		public virtual async Task UpdateAsync(M model)
		{
			var entity = await ByIdAsync(model.Id.Value);
			entity.Updation = ContextTools.Now();
			await UpdateAsync(model.Update(entity));
		}

		public virtual async Task UpdateAsync(IEnumerable<M> models)
		{
			//TODO: VALIDAR SE HÁ IDS REPETIDOS
			var entities = await ByIdsAsync(models.Select(m => m.Id.Value));
			var dictModels = models.ToDictionary(m => m.Id, m => m);
			var entitiesToUpdateTasks = new List<Task>();
			foreach(var entity in entities)
			{
				entity.Updation = ContextTools.Now();
				entitiesToUpdateTasks.Add(UpdateAsync(dictModels[entity.Id].Update(entity)));
			}
			await Task.WhenAll(entitiesToUpdateTasks.ToArray());
		}
	}
}
