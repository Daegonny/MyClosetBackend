using NHibernate;
using System;
using System.Data;
using Util.Extensions;

namespace Infra.NH
{
	public class NHUnitOfWork : IUnitOfWork
	{
		private ITransaction Transaction;
		protected ISessionFactory Factory;

		private bool Disposed;
		private bool Begun;
		private bool Scoped;

		public virtual bool IsReadOnly { get; private set; }
		public virtual int ID { get; set; }
		private static readonly object @lock = new object();
		public virtual ISession Session { get; private set; }

		public NHUnitOfWork(ISessionFactory factory)
		{
			Factory = factory;
		}

		public IUnitOfWork Begin()
		{
			IsReadOnly = false;
			DoBegin();
			return this;
		}

		public IUnitOfWork BeginReadOnly()
		{
			IsReadOnly = true;
			DoBegin();
			return this;

		}

		private IUnitOfWork DoBegin()
		{
			if (Begun) return this;

			Transaction?.Dispose();

			if (Session.IsNull())
				CreateSession();

			Begun = true;
			return this;
		}
		private IUnitOfWork CreateSession()
		{
			lock (@lock)
			{
				if (Session == null)
				{
					if (IsReadOnly)
					{
						Session = Factory.OpenSession();
						Session.FlushMode = FlushMode.Manual;
					}
					else
					{
						Session = Factory.OpenSession();
						Transaction = Session.BeginTransaction(IsolationLevel.ReadCommitted);
					}
				}
			}
			return this;
		}

		public IUnitOfWork Commit()
		{
			if (!IsReadOnly)
			{
				CheckIsDisposed();
				CheckHasBegun();

				if (Transaction.IsActive && !Transaction.WasRolledBack)
				{
					Transaction.Commit();
				}
			}
			Begun = false;
			return this;
		}

		public IUnitOfWork Rollback()
		{
			if (!IsReadOnly)
			{
				CheckIsDisposed();
				CheckHasBegun();

				if (Transaction.IsActive)
				{
					Transaction.Rollback();
					Session.Clear();
				}

				Begun = false;
			}
			return this;
		}

		public void Dispose()
		{
			if (Begun && !Disposed)
			{
				if (Scoped)
					Commit();

				if (Transaction != null && !IsReadOnly)
					Transaction.Dispose();

				if (Session != null)
					Session.Dispose();

				Session = null;

				Disposed = true;
			}
		}

		private IUnitOfWork CheckHasBegun()
		{
			if (!Begun)
			{
				throw new InvalidOperationException("Must call Begin() on the unit of work before committing");
			}
			return this;
		}

		private IUnitOfWork CheckIsDisposed()
		{
			if (Disposed)
			{
				throw new ObjectDisposedException(GetType().Name);
			}
			return this;
		}

		public virtual IUnitOfWork BeginScoped()
		{
			Scoped = true;
			IsReadOnly = false;
			DoBegin();

			return this;
		}

		public virtual IUnitOfWork Flush()
		{
			if (Session != null && !IsReadOnly)
				Session.Flush();

			return this;
		}
	}
}
