using Infra.Abstractions;
using MyCloset.Domain.Entities;
using System.Threading.Tasks;

namespace MyCloset.Infra.Abstractions.Repositories
{
	public interface ISecretCodes : IRepository<SecretCode>
	{
		Task<SecretCode> ByNameAsync(string name);
	}
}
