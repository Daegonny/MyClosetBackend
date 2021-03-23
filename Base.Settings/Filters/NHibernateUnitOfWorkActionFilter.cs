using Infra.NH;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Base.Settings.Filters
{
	public class NHibernateUnitOfWorkActionFilter : ActionFilterAttribute
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly string[] readVerbs = new string[] { "post", "put", "delete", "update" };

		public NHibernateUnitOfWorkActionFilter(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
			unitOfWork.ID = 23;
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (readVerbs.Contains(context.HttpContext.Request.Method.ToLower()))
				unitOfWork.Begin();
			else
				unitOfWork.BeginReadOnly();

			base.OnActionExecuting(context);
		}
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			if (!unitOfWork.IsReadOnly)
			{
				try
				{
					if (context.Exception == null || context.ExceptionHandled)
					{
						unitOfWork.Commit();
					}
					else
					{
						unitOfWork.Rollback();
					}
				}
				catch (Exception ex)
				{
					unitOfWork.Rollback();
					throw ex;
				}
				finally
				{
					unitOfWork.Dispose();
				}
			}
			base.OnActionExecuted(context);
		}
	}
}
