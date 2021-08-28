using MyCloset.Domain.Entities;
using MyCloset.Services.Abstractions.Seed;
using System.Threading.Tasks;

namespace MyCloset.Services.Abstractions.CrudServices
{
	public interface ISecretCodeService : ICrudService<SecretCode>
	{
		Task<bool> CheckAvailability(string name);
		Task Consume(string name);
	}
}
