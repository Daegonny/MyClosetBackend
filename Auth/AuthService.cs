using Auth.Abstractions;
using MyCloset.Infra.Abstractions.Repositories;
using System.Threading.Tasks;

namespace Auth
{
	public class AuthService : IAuthService
	{
		ITokenService TokenService { get; }
		IAccounts Accounts { get; }

		public AuthService(ITokenService tokenService, IAccounts accounts)
		{
			TokenService = tokenService;
			Accounts = accounts;
		}

		public async Task<string> Login(string email, string password) 
			=> TokenService.AddTokenTo(await Accounts.Login(email, password)).Token;
	}
}
