using Auth.Abstractions;
using Microsoft.AspNetCore.Http;
using MyCloset.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auth
{
	public class UserProvider : IUserProvider
	{
		IHttpContextAccessor Accessor { get; }
		public UserProvider(IHttpContextAccessor accessor)
		{
			Accessor = accessor;
		}

		public User GetLoggedUser()
		{
			var identity = Accessor.HttpContext.User.Identities.FirstOrDefault();
			return new User(identity);
		}
	}
}
