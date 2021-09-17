using Exceptions.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Exceptions.Auth
{
	public class UnauthorizedException : BaseException
	{
		protected UnauthorizedException(string message) : base(message) { }

		protected UnauthorizedException(string message, Exception inner)
			: base(message, inner) { }

		public override ObjectResult ToObjectResult()
			=> new ResponseResult(this).AsObjectResult(HttpStatusCode.Unauthorized);
	}
}