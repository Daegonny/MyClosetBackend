using Base.Domain;

namespace MyCloset.Domain.Entities
{
	public class Tag : Entity
	{
		public Tag() { }
		public Tag(long? id, string name)
		{
			Id = id;
			Name = name;
		}

		public override bool Equals(object obj)
		{
			return obj is Tag tag &&
				   base.Equals(obj) &&
				   Name == tag.Name;
		}
	}
}
