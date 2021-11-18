using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCloset.Domain.Models;
using MyCloset.Services.Abstractions.CrudServices;
using Resources;
using System.Threading.Tasks;

namespace API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AccountController : ControllerBase
	{
		IAccountService AccountService { get; }

		public AccountController(IAccountService accountService)
		{
			AccountService = accountService;
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<ActionResult<string>> Save(AccountModel model)
		{
			await AccountService.SaveAsync(model);
			return Ok(Resource.CreateAccountOk);
		}

		[HttpPut]
		[Authorize]
		public async Task<ActionResult<string>> Update(AccountModel model)
		{
			var token = await AccountService.UpdateAsync(model);
			var response = new SimpleResponse(token, Resource.UpdateAccountOk);
			return Ok(response);
		}

		[AllowAnonymous]
		[HttpGet("CheckAvailability")]
		public async Task<ActionResult<bool>> CheckAvailability(string email)
			=> Ok(await AccountService.EnsureNotExists(email));
	}
}
