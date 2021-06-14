using Base.Domain;
using Infra.Abstractions;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Services;

namespace Infra.NH
{
	public abstract class NHRepository<T, Q> : IRepository<T, Q>
		where T : Entity, new()
		where Q : IQueryFilter<T>
	{
		protected NHRepository(IUnitOfWork unitOfWork, IContextTools contextTools)
		{
			UnitOfWork = unitOfWork;
			ContextTools = contextTools;
		}

		IUnitOfWork UnitOfWork { get; }
		IContextTools ContextTools { get; }
		T Alias = null;

		protected virtual IQueryOver<T, T> Query()
			=> UnitOfWork.Session.QueryOver<T>(() => Alias);

		public async Task<IEnumerable<T>> AllAsync()
			=> await Query().ListAsync<T>();

		public async Task<IEnumerable<T>> AllValidAsync() 
			=> await Query().Where(x => x.Exclusion == null).ListAsync<T>();

		public async Task<T> ByIdAsync(long id) 
			=> await Query().Where(x => x.Id == id).SingleOrDefaultAsync<T>();

		public async Task<IEnumerable<T>> ByIdsAsync(IEnumerable<long> ids)
			=> await Query().WhereRestrictionOn(x => x.Id).IsInG(ids).ListAsync();

		public async Task DisableAsync(long id)
		{
			var entity = await ByIdAsync(id);
			entity.Exclusion = ContextTools.Now();
			await SaveAsync(entity);
		}

		public async Task RecoverAsync(long id)
		{
			var entity = await ByIdAsync(id);
			entity.Exclusion = null;
			await SaveAsync(entity);
		}

		public async Task RemoveAsync(long id) 
			=> await UnitOfWork.Session.DeleteAsync(new T() { Id = id });

		public async Task RemoveAsync(T entity)
			=> await UnitOfWork.Session.DeleteAsync(entity);

		public async Task<T> SaveAsync(T entity)
		{
			entity.Creation = ContextTools.Now();
			entity.Id = (long)await UnitOfWork.Session.SaveAsync(entity);
			return entity;
		} 

		public async Task<IEnumerable<T>> SaveAsync(IEnumerable<T> entities)
		{
			var savedEntities = new List<T>();
			foreach(var entity in entities)
			{
				savedEntities.Add(await SaveAsync(entity));
			}
			return savedEntities;
		}

		public async Task UpdateAsync(T entity)
			=> await UnitOfWork.Session.UpdateAsync(entity);

		public async Task<IEnumerable<T>> PagedAsync(int start, int quantity) 
			=> await Query().Skip(start).Take(quantity).ListAsync();

		public async Task<IEnumerable<T>> FilteredAsync(Q queryFilter, int start, int quantity) 
			=> await PagedAsync(queryFilter.ApplyFilters(Query()), start, quantity);

		public async Task<IEnumerable<T>> PagedAsync(IQueryOver<T,T> query, int start, int quantity)
			=> await query.OrderBy(q => q.Creation).Desc().Skip(start).Take(quantity).ListAsync();

		public async Task<int> FilteredRowCountAsync(Q queryFilter)
			=> await queryFilter.ApplyFilters(Query()).Select(Projections.CountDistinct(Projections.Id())).SingleOrDefaultAsync<int>();
	}
}
