using Base.Domain;
using NHibernate;

namespace Infra.Abstractions
{
	public interface IQueryFilter<T> where T : Entity
	{
		IQueryOver<T, T> ApplyFilters(IQueryOver<T, T> query);
	}
}
