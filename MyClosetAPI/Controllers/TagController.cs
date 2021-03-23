using Microsoft.AspNetCore.Mvc;
using MyCloset.Infra.Abstractions.Repositories;
using System.Threading.Tasks;

namespace MyClosetAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TagController : ControllerBase
	{
		public TagController(ITags tags)
		{
			Tags = tags;
		}

		ITags Tags { get; }

		[HttpGet]
		public async Task<ActionResult> All()
		{
			return Ok(await Tags.AllAsync());
		}
	}
}
