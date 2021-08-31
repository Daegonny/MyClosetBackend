using Auth.Abstractions;
using Infra.NH;
using MyCloset.Domain.Entities;
using MyCloset.Domain.Enums;
using MyCloset.Infra.Abstractions.Repositories;
using System.Threading.Tasks;
using Util.Services;

namespace MyCloset.Infra.NH.Repositories
{
	public class SecretCodes : NHRepository<SecretCode>, ISecretCodes
	{
		public SecretCodes(IUnitOfWork unitOfWork, IContextTools contextTools, IAccountProvider accountProvider) 
			: base(unitOfWork, contextTools, accountProvider)
		{
		}

		public async Task<SecretCode> ByNameAndTypeAsync(string name, SecretCodeType type)
			=> await Query()
				.Where(s => s.Name == name && s.Type == type)
				.SingleOrDefaultAsync();
	}
}
