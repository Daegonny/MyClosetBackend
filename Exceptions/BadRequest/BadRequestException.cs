using Exceptions.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Exceptions.BadRequest
{
	public class BadRequestException : BaseException
	{
		public BadRequestException(string message) : base(message) { }

		protected BadRequestException(string message, Exception inner)
			: base(message, inner) { }

		public override ObjectResult ToObjectResult()
			=> new ResponseResult(this).AsObjectResult(HttpStatusCode.BadRequest);
	}
}
