using Microsoft.AspNetCore.Mvc;
using MyCloset.Infra.Abstractions.Repositories;
using Util.Config;
using Util.Services;

namespace API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class FileController : ControllerBase
	{
		IContextTools ContextTools { get; }
		IFiles Files { get; }
		IImageFileConfig ImageFileConfig { get; }

		public FileController(IContextTools contextTools, IFiles files, 
			IImageFileConfig imageFileConfig)
		{
			ContextTools = contextTools;
			Files = files;
			ImageFileConfig = imageFileConfig;
		}

		[HttpGet("{path}/{file}")]
		public IActionResult Get(string path, string file) 
			=> File(Files.Read(ContextTools.DefaultBasePath(), path, file), ImageFileConfig.ContentType);
	}
}
