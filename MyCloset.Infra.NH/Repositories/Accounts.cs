using Auth.Abstractions;
using Infra.NH;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Infra.Abstractions.Repositories;
using System.Threading.Tasks;
using Util.Services;

namespace MyCloset.Infra.NH.Repositories
{
	public class Accounts : NHRepository<Account, IAccountQueryFilter>, IAccounts
	{
		public Accounts
		(
			IUnitOfWork unitOfWork, 
			IContextTools contextTools, 
			IAccountProvider accountProvider
		)	: base(unitOfWork, contextTools, accountProvider)
		{
		}

		public async Task<Account> ByEmail(string email) 
			=> await Query().Where(u => u.Email == email).SingleOrDefaultAsync();
	}
}
