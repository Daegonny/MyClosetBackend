using Resources;

namespace Exceptions.Auth
{
	public class LoginFailedException : UnauthorizedException
	{
		public LoginFailedException() : base(Resource.LoginFailed) {}
	}
}
