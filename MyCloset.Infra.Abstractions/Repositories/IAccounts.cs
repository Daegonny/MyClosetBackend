using Infra.Abstractions;
using MyCloset.Domain.Entities;
using System.Threading.Tasks;

namespace MyCloset.Infra.Abstractions.Repositories
{
	public interface IAccounts : IRepository<Account>
	{
		Task<Account> ByEmailAsync(string email);
	}
}
