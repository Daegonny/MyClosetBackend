using Exceptions.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCloset.Domain.Entities;
using MyCloset.Domain.Enums;
using MyCloset.Services.Abstractions.CrudServices;

using System.Threading.Tasks;
using Util.Config;
using Util.Extensions;
using Util.Services;

namespace API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SecretCodeController : ControllerBase
	{
		ISecretCodeService SecretCodeService { get; }
		IContextTools ContextTools { get; }
		IHashConfig HashConfig { get; }

		public SecretCodeController(ISecretCodeService secretCodeService, IContextTools contextTools, IHashConfig hashConfig)
		{
			SecretCodeService = secretCodeService;
			ContextTools = contextTools;
			HashConfig = hashConfig;
		}


		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult<string>> Create(string pass, string expiration)
		{
			if (pass != HashConfig.Secret)
				throw new InvalidPassException();

			var code = await SecretCodeService.Create(expiration.ToDate(), HashConfig.Secret);

			return Ok(code);
		}

		[AllowAnonymous]
		[HttpGet("CheckAvailability")]
		public async Task<ActionResult<bool>> CheckAvailability(string code, SecretCodeType type) 
			=> Ok(await SecretCodeService.CheckAvailability(code, type));
	}
}
