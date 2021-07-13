using Exceptions.Base;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Base.Settings.Filters
{
	public class HttpResponseExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			context.Result = context.Exception is BaseException
				? ((BaseException) context.Exception).ToObjectResult()
				: new ResponseResult(context.Exception).AsObjectResult(HttpStatusCode.InternalServerError);
			context.ExceptionHandled = true;
		}
	}
}
