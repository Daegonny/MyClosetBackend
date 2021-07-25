using Base.Domain;
using System;
using System.Linq;
using System.Security.Claims;

namespace MyCloset.Domain.Entities
{
	public class User : Entity
	{

		public virtual string Email {get; set;}
		public virtual string Password { get; set; }
		public virtual string Path { get; set; }
		public virtual string Token { get; private set; }
		public User AddToken(string token)
		{
			Token = token;
			return this;
		}

		public User() { }

		public User(ClaimsIdentity identity)
		{
			var claims = identity.Claims.ToDictionary(c => c.Type.ToString(), c => c.Value);
			Id = Convert.ToInt64(claims[nameof(Id)]);
			Name = claims[nameof(Name)];
			Email = claims[nameof(Email)];
			Path = claims[nameof(Path)];
		}
	}
}
