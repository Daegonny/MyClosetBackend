using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using Util.Extensions;

namespace MyClosetAPI.Settings
{
	public class ResponseResult
	{
		public IEnumerable<string> Messages { get; }

		public ResponseResult(Exception error = null)
		{
			Messages = error.Message.AsList();
		}

		public ObjectResult AsObjectResult(HttpStatusCode statusCode = HttpStatusCode.OK)
			=> new ObjectResult(this) { StatusCode = (int)statusCode };
	}
}
