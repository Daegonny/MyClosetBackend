using MyCloset.Domain.Entities;
using MyCloset.Domain.Enums;
using MyCloset.Services.Abstractions.Seed;
using System.Threading.Tasks;

namespace MyCloset.Services.Abstractions.CrudServices
{
	public interface ISecretCodeService : ICrudService<SecretCode>
	{
		Task<bool> CheckAvailability(string name, SecretCodeType type);
		Task Consume(Account account, string name, SecretCodeType type);
	}
}
