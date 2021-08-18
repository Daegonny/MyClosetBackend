using Base.Domain;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace MyCloset.Domain.Entities
{
	public class Account : Entity
	{
		public virtual string Email {get; set;}
		public virtual string Password { get; set; }
		[JsonIgnore]
		public virtual string HashedFilePath { get; set; }
		[JsonIgnore]
		public virtual string Token { get; protected set; }
		public virtual Account AddToken(string token)
		{
			Token = token;
			return this;
		}

		public Account() { }

		public Account(ClaimsIdentity identity)
		{
			var claims = identity.Claims.ToDictionary(c => c.Type.ToString(), c => c.Value);
			
			if (claims.ContainsKey(nameof(Id)))
				Id = Convert.ToInt64(claims[nameof(Id)]);
			
			if (claims.ContainsKey(nameof(Name)))
				Name = claims[nameof(Name)];
			
			if (claims.ContainsKey(nameof(Email)))
				Email = claims[nameof(Email)];
			
			if (claims.ContainsKey(nameof(HashedFilePath)))
				HashedFilePath = claims[nameof(HashedFilePath)];
		}
	}
}
