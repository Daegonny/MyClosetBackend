using MyCloset.Domain.Entities;
using MyCloset.Domain.Models;
using MyCloset.Services.Abstractions.Seed;
using System.Threading.Tasks;

namespace MyCloset.Services.Abstractions.CrudServices
{
	public interface IAccountService : ICrudService<Account>
	{
		Task SaveAsync(AccountModel model);
		Task<string> UpdateAsync(AccountModel model);
	}
}
