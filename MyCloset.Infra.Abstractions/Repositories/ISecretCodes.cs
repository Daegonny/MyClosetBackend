using Infra.Abstractions;
using MyCloset.Domain.Entities;
using MyCloset.Domain.Enums;
using System.Threading.Tasks;

namespace MyCloset.Infra.Abstractions.Repositories
{
	public interface ISecretCodes : IRepository<SecretCode>
	{
		Task<SecretCode> ByNameAndTypeAsync(string name, SecretCodeType type);
	}
}
