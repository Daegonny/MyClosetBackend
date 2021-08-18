using MyCloset.Domain.Entities;
using MyCloset.Domain.Models;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Infra.Abstractions.Repositories;
using MyCloset.Services.Abstractions.CrudServices;
using MyCloset.Services.Seed;
using System.Threading.Tasks;
using Util.Services;

namespace MyCloset.Services.CrudServices
{
	public class AccountService : CrudService<Account, IAccountQueryFilter>, IAccountService
	{
		public AccountService(IAccounts accounts, IContextTools contextTools) 
			: base(accounts, contextTools)
		{
		}

		public async Task SaveAsync(AccountModel model)
		{

		}

		public async Task UpdateAsync(AccountModel model)
		{

		}
	}
}
