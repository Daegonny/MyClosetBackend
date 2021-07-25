using MyCloset.Domain.Entities;

namespace Auth.Abstractions
{
	public interface ITokenService
	{
		Account AddTokenTo(Account user);
	}
}
