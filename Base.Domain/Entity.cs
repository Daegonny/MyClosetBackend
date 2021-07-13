using System;
using System.Text.Json.Serialization;

namespace Base.Domain
{
	public class Entity
	{
		public virtual long? Id { get; set; }
		public virtual string Name { get; set; }
		[JsonIgnore]
		public virtual DateTime? Creation { get; set; }
		[JsonIgnore]
		public virtual DateTime? LastUpdate { get; set; }
		[JsonIgnore]
		public virtual DateTime? Exclusion { get; set; }

		public static bool operator == (Entity x, Entity y)
		{
			if (x is null || y is null)
				return x is null && y is null;
			else
			{
				var equalTypes = x.GetType() == y.GetType() || x.GetType().Name.Replace("Proxy", String.Empty) == y.GetType().Name.Replace("Proxy", String.Empty);
				if (x.Id != 0)
					return x.Id == y.Id && equalTypes;
				else
					return x.GetHashCode() == y.GetHashCode() && x.Id == y.Id && equalTypes;
			}
		}
		public static bool operator !=(Entity x, Entity y)
		{
			return !(x == y);
		}
		public override bool Equals(object obj)
		{
			if (obj is Entity)
				return ((Entity)obj) == this;
			else
				return base.Equals(obj);
		}
		public virtual bool Equals(Entity other)
		{
			return other.Id == Id;
		}
		public virtual bool Equals(Entity x, Entity y)
		{
			return x == y;
		}
		public virtual int Compare(Entity x, Entity y)
		{
			return x == y ? 0 : 1;
		}
		
		public virtual int GetHashCode(Entity obj)
		{
			return obj.Id.GetHashCode();
		}
		public virtual dynamic Simplify()
		{
			return new { Id, Name };
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Name);
		}
	}
}
