using Resources;

namespace Exceptions.Auth
{
	public class AccessDeniedException : ForbiddenException
	{
		public AccessDeniedException() : base (Resource.AccessDenied) {}
	}
}
