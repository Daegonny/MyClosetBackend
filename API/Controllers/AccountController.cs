using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCloset.Domain.Models;
using MyCloset.Services.Abstractions.CrudServices;
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
		public async Task<ActionResult<string>> Save(AccountModel model)
		{
			await AccountService.SaveAsync(model);
			return Ok(); //TODO: Adicionar retorno de sucesso
		}


		[HttpPut]
		[Authorize]
		public async Task<ActionResult<string>> Update(AccountModel model)
		{
			await AccountService.UpdateAsync(model);
			return Ok(); //TODO: Adicionar retorno de sucesso
		}
	}
}
