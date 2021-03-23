using NHibernate;
using System;

namespace Infra.NH
{
	public interface IUnitOfWork : IDisposable
	{
		IUnitOfWork Begin();
		IUnitOfWork BeginScoped();
		IUnitOfWork BeginReadOnly();
		IUnitOfWork Flush();
		ISession Session { get; }
		IUnitOfWork Commit();
		IUnitOfWork Rollback();
		bool IsReadOnly { get; }
		int ID { get; set; }
	}
}
