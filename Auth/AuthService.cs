using Auth.Abstractions;
using MyCloset.Domain.Entities;
using System;

namespace Auth
{
	public class AuthService : IAuthService
	{
		ITokenService TokenService { get; }
		public AuthService(ITokenService tokenService)
		{
			TokenService = tokenService;
		}

		public User Login(string userName, string password)
		{
			throw new NotImplementedException();
		}
	}
}
