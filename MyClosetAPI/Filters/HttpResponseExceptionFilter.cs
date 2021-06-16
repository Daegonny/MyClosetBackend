using Microsoft.AspNetCore.Mvc.Filters;
using MyClosetAPI.Settings;
using System.Net;

namespace MyClosetAPI.Filters
{
	public class HttpResponseExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			context.Result = new ResponseResult(context.Exception).AsObjectResult(HttpStatusCode.InternalServerError);
			context.ExceptionHandled = true;
		}
	}
}
