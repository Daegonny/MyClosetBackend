using Auth.Abstractions;
using Exceptions.BadRequest;
using Exceptions.NotFound;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.Repositories;
using MyCloset.Services.Abstractions.CrudServices;
using MyCloset.Services.Seed;
using Resources;
using System.Threading.Tasks;
using Util.Extensions;
using Util.Services;

namespace MyCloset.Services.CrudServices
{
	public class SecretCodeService : CrudService<SecretCode>, ISecretCodeService
	{
		ISecretCodes SecretCodes { get; }
		IAccountProvider AccountProvider { get; }
		IContextTools ContextTools { get; }

		public SecretCodeService
		(
			ISecretCodes secretCodes,
			IAccountProvider accountProvider,
			IContextTools contextTools
		) 
			: base(secretCodes, contextTools)
		{
			SecretCodes = secretCodes;
			AccountProvider = accountProvider;
			ContextTools = contextTools;
		}

		public async Task Consume(string name)
		{
			var secretCode = await SecretCodes.ByNameAsync(name);
			CheckAvailability(secretCode);
			secretCode.Account = AccountProvider.GetLoggedUser();
			secretCode.Activation = ContextTools.Now();
			await UpdateAsync(secretCode);
		}

		public async Task<bool> CheckAvailability(string name) 
			=> CheckAvailability(await SecretCodes.ByNameAsync(name));

		bool CheckAvailability(SecretCode secretCode)
		{
			if (secretCode.IsNull())
				throw new EntityNotFoundException(Resource.SecredCodeNotFound);

			if (secretCode.IsExpired(ContextTools.Now()))
				throw new BadRequestException(Resource.SecretCodeExpired);

			if (secretCode.IsConsumed())
				throw new BadRequestException(Resource.SecretCodeUnavailable);

			return true;
		}
	}
}
