using Resources;

namespace Exceptions.Auth
{
	public class InvalidPassException: UnauthorizedException
	{
		public InvalidPassException() : base(Resource.InvalidPass) { }
	}
}
