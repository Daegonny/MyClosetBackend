using Exceptions.Auth;
using MyCloset.Domain.Entities;
using MyCloset.Domain.Models;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Infra.Abstractions.Repositories;
using MyCloset.Services.Abstractions.CrudServices;
using MyCloset.Services.Seed;
using System.Threading.Tasks;
using Util.Config;
using Util.Extensions;
using Util.Services;

namespace MyCloset.Services.CrudServices
{
	public class AccountService : CrudService<Account, IAccountQueryFilter>, IAccountService
	{
		IAccounts Accounts { get; }
		IHashConfig HashConfig { get; }
		IContextTools ContextTools { get; }

		public AccountService
		(
			IAccounts accounts,
			IHashConfig hashConfig,
			IContextTools contextTools
		) 
			: base(accounts, contextTools)
		{
			Accounts = accounts;
			HashConfig = hashConfig;
			ContextTools = contextTools;
		}


		public async Task SaveAsync(AccountModel model)
		{
			await Validate(model);

			var account = model.ToEntity(ContextTools.Now(), HashConfig.Secret);
			//TODO: CREATE FOLDER ON FILE SYSTEM
			await SaveAsync(account);
		}

		public Task UpdateAsync(AccountModel model)
		{
			return Task.Delay(5);
		}

		async Task Validate(AccountModel model)
		{
			var account = await Accounts.ByEmail(model.Email);
			if (account.NotNull())
				throw new AccountAlreadyExistsException();

			if (model.Email != model.EmailConfirm)
				throw new AccountAlreadyExistsException(); //TODO: Mudar tipo de exceção

			if (model.Password != model.PasswordConfirm)
				throw new AccountAlreadyExistsException(); //TODO: Mudar tipo de exceção

			//TODO: validar secretCode
		}
	}
}
