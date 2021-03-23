using System.Collections.Generic;

namespace Base.Domain
{
	public class TaggeableEntity<T> : Entity
		where T : Entity
	{
		public virtual IEnumerable<T> Tags { get; set; }
	}
}
