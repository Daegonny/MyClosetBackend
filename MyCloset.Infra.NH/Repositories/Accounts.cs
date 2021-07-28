using Auth.Abstractions;
using Exceptions.Auth;
using Infra.NH;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Infra.Abstractions.Repositories;
using System.Threading.Tasks;
using Util.Config;
using Util.Extensions;
using Util.Services;

namespace MyCloset.Infra.NH.Repositories
{
	public class Accounts : NHRepository<Account, IAccountQueryFilter>, IAccounts
	{
		IHashConfig HashConfig { get; }
		public Accounts
		(
			IUnitOfWork unitOfWork, 
			IContextTools contextTools, 
			IAccountProvider accountProvider, 
			IHashConfig hashConfig
		)	: base(unitOfWork, contextTools, accountProvider)
		{
			HashConfig = hashConfig;
		}


		public async Task<Account> Login(string email, string password)
		{
			var account = await Query().Where(u => u.Email == email).SingleOrDefaultAsync();
			var hashedPassword = $"{password}{account.Creation}{HashConfig.Salt}".Hash();
			if (account.IsNull() || account.Password != hashedPassword)
				throw new LoginFailedException();
			return account;
		}
	}
}
