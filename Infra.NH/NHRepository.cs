using Auth.Abstractions;
using Base.Domain;
using Infra.Abstractions;
using MyCloset.Domain.Entities;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util.Extensions;
using Util.Services;

namespace Infra.NH
{
	public abstract class NHRepository<T> : IRepository<T>
		where T : Entity, new()
	{
		IUnitOfWork UnitOfWork { get; }
		IContextTools ContextTools { get; }
		Account LoggedUser { get; }

		T Alias = null;
		protected NHRepository
		(
			IUnitOfWork unitOfWork, 
			IContextTools contextTools, 
			IAccountProvider accountProvider
		)
		{
			UnitOfWork = unitOfWork;
			ContextTools = contextTools;
			LoggedUser = accountProvider.GetLoggedUser();
		}

		protected virtual IQueryOver<T, T> QueryFilteringOwner()
		{
			return typeof(IHaveOwner).IsAssignableFrom(typeof(T))
				 ? UnitOfWork.Session.QueryOver<T>(() => Alias)
					.Where(T => ((IHaveOwner) T).Account.Id == LoggedUser.Id)
				: Query();
		}

		protected virtual IQueryOver<T, T> Query() 
			=> UnitOfWork.Session.QueryOver<T>(() => Alias);

		public async Task<T> ByIdAsync(long id) 
			=> await QueryFilteringOwner().Where(x => x.Id == id).SingleOrDefaultAsync<T>();

		public async Task<IEnumerable<T>> ByIdsAsync(IEnumerable<long> ids)
			=> await QueryFilteringOwner().WhereRestrictionOn(x => x.Id).IsInG(ids).ListAsync();

		public async Task RemoveAsync(T entity)
			=> await UnitOfWork.Session.DeleteAsync(entity);

		public async Task<T> SaveAsync(T entity)
		{
			if (entity.Creation.IsNull())
				entity.Creation = ContextTools.Now();
			
			entity.Id = (long)await UnitOfWork.Session.SaveAsync(entity);
			return entity;
		} 

		public async Task<IEnumerable<T>> SaveAsync(IEnumerable<T> entities)
		{
			var savedEntities = new List<T>();
			foreach(var entity in entities)
				savedEntities.Add(await SaveAsync(entity));
			return savedEntities;
		}

		public async Task UpdateAsync(T entity)
			=> await UnitOfWork.Session.UpdateAsync(entity);	
	}
}
