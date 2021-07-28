using Auth.Abstractions;
using Microsoft.AspNetCore.Http;
using MyCloset.Domain.Entities;
using System.Linq;

namespace Auth
{
	public class AccountProvider : IAccountProvider
	{
		IHttpContextAccessor Accessor { get; }
		public AccountProvider(IHttpContextAccessor accessor)
		{
			Accessor = accessor;
		}

		public Account GetLoggedUser() 
			=> new Account(Accessor.HttpContext.User.Identities.FirstOrDefault());
	}
}
