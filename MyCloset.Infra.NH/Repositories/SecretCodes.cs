using Auth.Abstractions;
using Infra.NH;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.Repositories;
using Util.Services;

namespace MyCloset.Infra.NH.Repositories
{
	public class SecretCodes : NHRepository<SecretCode>, ISecretCodes
	{
		public SecretCodes(IUnitOfWork unitOfWork, IContextTools contextTools, IAccountProvider accountProvider) 
			: base(unitOfWork, contextTools, accountProvider)
		{
		}
	}
}
