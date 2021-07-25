using Exceptions.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Exceptions.Auth
{
	public class ForbiddenException : BaseException
	{
		protected ForbiddenException(string message) : base(message) { }

		protected ForbiddenException(string message, Exception inner)
			: base(message, inner) { }

		public override ObjectResult ToObjectResult()
			=> new ResponseResult(this).AsObjectResult(HttpStatusCode.Forbidden);
	}
}