using Resources;
using System.Collections.Generic;

namespace Exceptions.NotFound
{
	public class EntityNotFoundException : NotFoundException
	{
		public EntityNotFoundException(string message) : base(message) {}
		public EntityNotFoundException(long id)
			: base(string.Format(Resource.EntityNotFoundSingle, id)) { }

		public EntityNotFoundException(IEnumerable<long> ids)
			: base(string.Format(Resource.EntityNotFoundMultiple, string.Join(", ", ids))) { }
	}
}
