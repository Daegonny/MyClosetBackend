using Microsoft.AspNetCore.Mvc;
using Resources;
using System.Collections.Generic;
using System.Net;

namespace Exceptions
{
	public class NotFoundException : BaseException
	{
		public NotFoundException(string message) : base(message) {}

		public NotFoundException(long id) 
			: base(string.Format(Resource.NotFoundSingle, id)) { }

		public NotFoundException(IEnumerable<long> ids)
			: base(string.Format(Resource.NotFoundSingle, string.Join(", ", ids))) { }

		public override ObjectResult ToObjectResult() 
			=> new ResponseResult(this).AsObjectResult(HttpStatusCode.NotFound);
	}
}
