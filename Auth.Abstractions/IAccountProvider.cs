
using MyCloset.Domain.Entities;

namespace Auth.Abstractions
{
	public interface IAccountProvider
	{
		Account GetLoggedUser();
	}
}
