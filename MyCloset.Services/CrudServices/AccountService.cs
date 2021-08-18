using Exceptions.Auth;
using Exceptions.BadRequest;
using MyCloset.Domain.Entities;
using MyCloset.Domain.Models;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Infra.Abstractions.Repositories;
using MyCloset.Services.Abstractions.CrudServices;
using MyCloset.Services.Seed;
using Resources;
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
		IFiles Files { get; }
		IContextTools ContextTools { get; }

		public AccountService
		(
			IAccounts accounts,
			IHashConfig hashConfig,
			IFiles files,
			IContextTools contextTools
		) 
			: base(accounts, contextTools)
		{
			Accounts = accounts;
			HashConfig = hashConfig;
			Files = files;
			ContextTools = contextTools;
		}


		public async Task SaveAsync(AccountModel model)
		{
			await Validate(model);

			var account = model.ToEntity(ContextTools.Now(), HashConfig.Secret);
			Files.CreateDirectory(ContextTools.DefaultBasePath(), account.HashedFilePath);
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
				throw new BadRequestException(Resource.EmailNotEqualsConfirmation);

			if (model.Password != model.PasswordConfirm)
				throw new BadRequestException(Resource.PasswordNotEqualsConfirmation);

			//TODO: validar secretCode
		}
	}
}
