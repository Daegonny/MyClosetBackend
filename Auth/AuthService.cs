using Auth.Abstractions;
using MyCloset.Infra.Abstractions.Repositories;
using System.Threading.Tasks;

namespace Auth
{
	public class AuthService : IAuthService
	{
		ITokenService TokenService { get; }
		IAccounts Users { get; }

		public AuthService(ITokenService tokenService, IAccounts users)
		{
			TokenService = tokenService;
			Users = users;
		}

		public async Task<string> Login(string email, string password) 
			=> TokenService.AddTokenTo(await Users.Login(email, password)).Token;
	}
}
