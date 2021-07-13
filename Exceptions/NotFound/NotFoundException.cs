using Exceptions.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Exceptions.NotFound
{
	public class NotFoundException : BaseException
	{
		protected NotFoundException(string message) : base(message) {}

		protected NotFoundException(string message, Exception inner) 
			: base(message, inner) { }

		public override ObjectResult ToObjectResult() 
			=> new ResponseResult(this).AsObjectResult(HttpStatusCode.NotFound);
	}
}
