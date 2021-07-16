using Auth.Abstractions;
using MyCloset.Infra.Abstractions.Repositories;
using System.Threading.Tasks;

namespace Auth
{
	public class AuthService : IAuthService
	{
		ITokenService TokenService { get; }
		IUsers Users { get; }

		public AuthService(ITokenService tokenService, IUsers users)
		{
			TokenService = tokenService;
			Users = users;
		}

		public async Task<string> Login(string userName, string password) 
			=> TokenService.AddTokenTo(await Users.Login(userName, password)).Token;
	}
}
