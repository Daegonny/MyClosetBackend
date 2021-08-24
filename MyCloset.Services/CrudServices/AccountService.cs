using Auth.Abstractions;
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
		IAccountProvider AccountProvider { get; }
		ITokenService TokenService { get; }
		IContextTools ContextTools { get; }

		public AccountService
		(
			IAccounts accounts,
			IHashConfig hashConfig,
			IFiles files,
			IAccountProvider accountProvider,
			ITokenService tokenService,
			IContextTools contextTools
		) 
			: base(accounts, contextTools)
		{
			Accounts = accounts;
			HashConfig = hashConfig;
			Files = files;
			AccountProvider = accountProvider;
			TokenService = tokenService;
			ContextTools = contextTools;
		}


		public async Task SaveAsync(AccountModel model)
		{
			await ValidateOnSave(model);

			var account = model.ToEntity(ContextTools.Now(), HashConfig.Secret);
			Files.CreateDirectory(ContextTools.DefaultBasePath(), account.HashedFilePath);
			await SaveAsync(account);
		}

		public async Task<string> UpdateAsync(AccountModel model)
		{
			var account = await ByIdAsync(AccountProvider.GetLoggedUser().Id.Value);
			await ValidateOnUpdate(model, account);
			model.Update(account, HashConfig.Secret);
			await UpdateAsync(account);
			return TokenService.AddTokenTo(account).Token;
		}

		async Task ValidateOnUpdate (AccountModel model, Account account)
		{
			ValidateModel(model);
			if (model.Email != account.Email)
				if (await AccountExists(model.Email))
					throw new AccountAlreadyExistsException();
		}

		async Task ValidateOnSave(AccountModel model)
		{
			ValidateModel(model);

			if (await AccountExists(model.Email))
				throw new AccountAlreadyExistsException();
			
			//TODO: validar secretCode
		}

		void ValidateModel(AccountModel model)
		{
			if (model.Email != model.EmailConfirm)
				throw new BadRequestException(Resource.EmailNotEqualsConfirmation);

			if (model.Password != model.PasswordConfirm)
				throw new BadRequestException(Resource.PasswordNotEqualsConfirmation);
		}

		public async Task<bool> AccountExists(string email)
		{
			return (await Accounts.ByEmailAsync(email)).NotNull();
		}
	}
}
