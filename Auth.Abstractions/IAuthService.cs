using MyCloset.Domain.Entities;
using System;

namespace Auth.Abstractions
{
	public interface IAuthService
	{
		public User Login(string userName, string password);
	}
}
