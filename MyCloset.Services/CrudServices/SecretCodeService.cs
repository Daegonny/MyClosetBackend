using Exceptions.BadRequest;
using Exceptions.NotFound;
using MyCloset.Domain.Entities;
using MyCloset.Domain.Enums;
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
		IContextTools ContextTools { get; }

		public SecretCodeService
		(
			ISecretCodes secretCodes,
			IContextTools contextTools
		) 
			: base(secretCodes, contextTools)
		{
			SecretCodes = secretCodes;
			ContextTools = contextTools;
		}

		public async Task Consume(Account account, string name, SecretCodeType type)
		{
			var secretCode = await SecretCodes.ByNameAndTypeAsync(name, type);
			CheckAvailability(secretCode);
			secretCode.Account = account;
			secretCode.Activation = ContextTools.Now();
			await UpdateAsync(secretCode);
		}

		public async Task<bool> CheckAvailability(string name, SecretCodeType type) 
			=> CheckAvailability(await SecretCodes.ByNameAndTypeAsync(name, type));

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
