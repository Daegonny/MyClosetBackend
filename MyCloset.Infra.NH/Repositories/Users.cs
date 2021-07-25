using Infra.NH;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Infra.Abstractions.Repositories;
using System.Threading.Tasks;
using Util.Services;

namespace MyCloset.Infra.NH.Repositories
{
	public class Users : NHRepository<User, IUserQueryFilter>, IUsers
	{
		public Users(IUnitOfWork unitOfWork, IContextTools contextTools) : base(unitOfWork, contextTools)
		{
		}

		public Task<User> Login(string name, string password) //TODO: criptografar password
		{
			return Task.FromResult(new User() { Name = "Igor", Id = 1, Email = "igorchagasm@hotmail.com", Path = "mycloset" });
		}
			//=> await Query().Where(u => u.Name == name && u.Password == password).SingleOrDefaultAsync();
	}
}
