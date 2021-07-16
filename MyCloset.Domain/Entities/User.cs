using Base.Domain;

namespace MyCloset.Domain.Entities
{
	public class User : Entity
	{
		public virtual string Password { get; set; }
		public virtual string Token { get; private set; }
		public User AddToken(string token)
		{
			Token = token;
			return this;
		}
	}
}
