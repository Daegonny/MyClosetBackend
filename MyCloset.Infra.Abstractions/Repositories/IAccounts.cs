using Infra.Abstractions;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.QueryFilters;
using System.Threading.Tasks;

namespace MyCloset.Infra.Abstractions.Repositories
{
	public interface IAccounts : IRepository<Account, IAccountQueryFilter>
	{
		Task<Account> ByEmail(string email);
	}
}
