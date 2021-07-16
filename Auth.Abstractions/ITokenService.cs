using MyCloset.Domain.Entities;

namespace Auth.Abstractions
{
	public interface ITokenService
	{
		void AddTo(User user);
	}
}
