using API.Models;
using Auth.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthController : ControllerBase 
	{
		IAuthService AuthService { get; }
		
		public AuthController(IAuthService authService) { AuthService = authService; }


		[HttpPost("login")]
		public async Task<ActionResult<string>> Login(LoginModel loginModel) 
			=> Ok(await AuthService.Login(loginModel.userName, loginModel.password));

	}
}
