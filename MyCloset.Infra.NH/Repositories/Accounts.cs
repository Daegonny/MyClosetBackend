using Auth.Abstractions;
using Infra.NH;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.Repositories;
using System.Threading.Tasks;
using Util.Services;

namespace MyCloset.Infra.NH.Repositories
{
	public class Accounts : NHRepository<Account>, IAccounts
	{
		public Accounts
		(
			IUnitOfWork unitOfWork, 
			IContextTools contextTools, 
			IAccountProvider accountProvider
		)	: base(unitOfWork, contextTools, accountProvider)
		{
		}

		public async Task<Account> ByEmailAsync(string email) 
			=> await Query().Where(u => u.Email == email).SingleOrDefaultAsync();
	}
}
