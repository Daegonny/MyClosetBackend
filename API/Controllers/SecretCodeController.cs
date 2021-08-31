
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCloset.Domain.Entities;
using MyCloset.Domain.Enums;
using MyCloset.Services.Abstractions.CrudServices;
using System;
using System.Collections.Generic;
using System.Linq;
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
		public async Task<ActionResult<string>> Create(string expiration)
		{
			var secretCode = new SecretCode()
			{
				Name = "código".Encrypt(ContextTools.Now(), HashConfig.Secret),
				Type = SecretCodeType.CreateAccount,
				Expiration = expiration.ToDate()
			};

			await SecretCodeService.SaveAsync(secretCode);
			return Ok(secretCode.Name);
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<ActionResult> CheckAvailability(string code, SecretCodeType type)
		{
			return Ok(await SecretCodeService.CheckAvailability(code, type));
		}
	}
}
