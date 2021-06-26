using Microsoft.AspNetCore.Mvc;
using System;

namespace Exceptions
{
	public abstract class BaseException : Exception
	{
		protected BaseException() : base() { }
		protected BaseException(string message) : base(message) { }
		protected BaseException(string message, Exception inner) : base(message, inner) {}

		public abstract ObjectResult ToObjectResult();
	}
}
