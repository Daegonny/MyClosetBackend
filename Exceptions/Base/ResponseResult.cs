using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Exceptions.Base
{
	public class ResponseResult
	{
		public string Type { get; }
		public string Message { get; }
		public string InnerType { get; }
		public string InnerMessage { get; }

		public ResponseResult(Exception error)
		{
			Type = error.GetType().ToString();
			Message = error.Message;
			InnerType = error.InnerException?.GetType().ToString();
			InnerMessage = error.InnerException?.Message;
		}

		public ObjectResult AsObjectResult(HttpStatusCode statusCode = HttpStatusCode.OK)
			=> new ObjectResult(this) { StatusCode = (int)statusCode };
	}
}
