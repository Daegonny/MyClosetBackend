
using MyCloset.Domain.Entities;
using System.Security.Principal;

namespace Auth.Abstractions
{
	public interface IUserProvider
	{
		User GetLoggedUser();
	}
}
