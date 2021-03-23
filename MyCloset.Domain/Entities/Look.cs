using Base.Domain;
using System.Collections.Generic;

namespace MyCloset.Domain.Entities
{
	public class Look : TaggeableEntity<Tag>
	{
		public virtual IEnumerable<Piece> Pieces { get; set; }
	}
}
