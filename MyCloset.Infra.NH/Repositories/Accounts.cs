using Auth.Abstractions;
using Infra.NH;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Infra.Abstractions.Repositories;
using System.Threading.Tasks;
using Util.Extensions;
using Util.Services;

namespace MyCloset.Infra.NH.Repositories
{
	public class Accounts : NHRepository<Account, IAccountQueryFilter>, IAccounts
	{
		public Accounts(IUnitOfWork unitOfWork, IContextTools contextTools, IAccountProvider accountProvider ) 
			: base(unitOfWork, contextTools, accountProvider)
		{
		}

		public async Task<Account> Login(string email, string password) //TODO: criptografar password
		{
			var account = await Query().Where(u => u.Email == email && u.Password == password).SingleOrDefaultAsync();
			if (account.IsNull())
				throw new System.Exception("aaaaaaaaa");
			return account;
		}
	}
}
