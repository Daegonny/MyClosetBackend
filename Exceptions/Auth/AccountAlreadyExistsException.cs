using Resources;

namespace Exceptions.Auth
{
	public class AccountAlreadyExistsException : ForbiddenException
	{
		public AccountAlreadyExistsException() : base(Resource.AccountAlreadyExists) { }
	}
}
