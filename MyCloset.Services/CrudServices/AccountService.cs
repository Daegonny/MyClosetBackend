using Auth.Abstractions;
using Exceptions.Auth;
using MyCloset.Domain.Entities;
using MyCloset.Domain.Enums;
using MyCloset.Domain.Models;
using MyCloset.Infra.Abstractions.Repositories;
using MyCloset.Services.Abstractions.CrudServices;
using MyCloset.Services.Seed;
using System.Threading.Tasks;
using Util.Config;
using Util.Extensions;
using Util.Services;

namespace MyCloset.Services.CrudServices
{
	public class AccountService : CrudService<Account>, IAccountService
	{
		IAccounts Accounts { get; }
		IHashConfig HashConfig { get; }
		IFiles Files { get; }
		IAccountProvider AccountProvider { get; }
		ITokenService TokenService { get; }
		ISecretCodeService SecretCodeService { get; }
		IContextTools ContextTools { get; }

		public AccountService
		(
			IAccounts accounts,
			IHashConfig hashConfig,
			IFiles files,
			IAccountProvider accountProvider,
			ITokenService tokenService,
			ISecretCodeService secretCodeService,
			IContextTools contextTools
		) 
			: base(accounts, contextTools)
		{
			Accounts = accounts;
			HashConfig = hashConfig;
			Files = files;
			AccountProvider = accountProvider;
			TokenService = tokenService;
			SecretCodeService = secretCodeService;
			ContextTools = contextTools;
		}

		public async Task SaveAsync(AccountModel model)
		{
			await ValidateOnSave(model);

			var account = model.ToEntity(ContextTools.Now(), HashConfig.Secret);
			await SecretCodeService.Consume(account, model.SecretCode, SecretCodeType.CreateAccount);

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
			model.Validate();
			if (model.Email != account.Email)
				await EnsureNotExists(model.Email);
		}

		async Task ValidateOnSave(AccountModel model)
		{
			model.Validate();
			await EnsureNotExists(model.Email);
		}

		public async Task<bool> EnsureNotExists(string email)
		{
			var account = await Accounts.ByEmailAsync(email);
			if(account.NotNull())
				throw new AccountAlreadyExistsException();
			return true;
		}
	}
}
