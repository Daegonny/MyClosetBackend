using Infra.Abstractions;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.QueryFilters;
using System.Threading.Tasks;

namespace MyCloset.Infra.Abstractions.Repositories
{
	public interface IUsers : IRepository<User, IUserQueryFilter>
	{
		Task<User> Login(string name, string password);
	}
}
