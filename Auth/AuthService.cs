using Auth.Abstractions;
using Exceptions.Auth;
using MyCloset.Infra.Abstractions.Repositories;
using System.Threading.Tasks;
using Util.Config;
using Util.Extensions;

namespace Auth
{
	public class AuthService : IAuthService
	{
		ITokenService TokenService { get; }
		IHashConfig HashConfig { get; }
		IAccounts Accounts { get; }

		public AuthService(ITokenService tokenService, IHashConfig hashConfig, IAccounts accounts)
		{
			TokenService = tokenService;
			HashConfig = hashConfig;
			Accounts = accounts;
		}

		public async Task<string> Login(string email, string password)
		{
			var account = await Accounts.ByEmailAsync(email);
			if (account.IsNull() || account.Password != password.Encrypt(account.Creation, HashConfig.Secret))
				throw new LoginFailedException();
			return TokenService.AddTokenTo(account).Token;
		} 
	}
}
