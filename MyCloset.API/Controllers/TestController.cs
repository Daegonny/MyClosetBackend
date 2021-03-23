using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyCloset.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TestController : ControllerBase
	{
		
		private readonly ILogger<TestController> _logger;

		public TestController(ILogger<TestController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public string Get()
		{
			return "Its a test, dont worry!";
		}
	}
}
