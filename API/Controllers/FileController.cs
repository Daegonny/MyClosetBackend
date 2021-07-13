using Microsoft.AspNetCore.Mvc;
using MyCloset.Infra.Abstractions.Repositories;
using Util.Services;

namespace API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class FileController : ControllerBase
	{
		private readonly IFiles Files;

		public FileController(IContextTools contextTools, IFiles files)
		{
			ContextTools = contextTools;
			Files = files;
		}

		IContextTools ContextTools { get; }

		[HttpGet("{path}/{file}")]
		public IActionResult Get(string path, string file) 
			=> File(Files.Read(ContextTools.DefaultBasePath(), path, file), ContextTools.DefaultFileType());
	}
}
