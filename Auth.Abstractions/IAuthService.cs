using System.Threading.Tasks;

namespace Auth.Abstractions
{
	public interface IAuthService
	{
		public Task<string> Login(string userName, string password);
	}
}
