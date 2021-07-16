using MyCloset.Domain.Entities;

namespace Auth.Abstractions
{
	public interface ITokenService
	{
		User AddTokenTo(User user);
	}
}
