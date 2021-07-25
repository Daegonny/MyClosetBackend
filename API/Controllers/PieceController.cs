using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCloset.Domain.Entities;
using MyCloset.Domain.Models;
using MyCloset.Infra.NH.QueryFilters;
using MyCloset.Services.Abstractions.CrudServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Services;

namespace API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[Authorize]
	public class PieceController : ControllerBase
	{
		public PieceController(IContextTools contextTools, IPieceService pieceService)
		{
			ContextTools = contextTools;
			PieceService = pieceService;
		}

		IContextTools ContextTools { get; }
		IPieceService PieceService { get; }

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Piece>>> All() 
			=> Ok(await PieceService.AllAsync());

		[HttpGet("Filtered")]
		public async Task<ActionResult<IEnumerable<Piece>>> Filtered
		(
			[FromQuery] int start,
			[FromQuery] int quantity,
			[FromQuery] string name,
			[FromQuery] DateTime? PurchaseDateMin,
			[FromQuery] DateTime? PurchaseDateMax,
			[FromQuery] decimal? PriceMin,
			[FromQuery] decimal? PriceMax,
			[FromQuery] string[] TagNames,
			[FromQuery] bool MustHaveAllTagNames
		)
		{
			var queryFilter = new PieceQueryFilter(
				name, PurchaseDateMin, PurchaseDateMax, PriceMin, PriceMax, TagNames, MustHaveAllTagNames);
			return Ok(await PieceService.FilteredAsync(queryFilter, start, quantity));
		}

		[HttpGet("Filtered/RowCount")]
		public async Task<ActionResult<int>> FilteredRowCount
		(
			[FromQuery] string name,
			[FromQuery] DateTime? PurchaseDateMin,
			[FromQuery] DateTime? PurchaseDateMax,
			[FromQuery] decimal? PriceMin,
			[FromQuery] decimal? PriceMax,
			[FromQuery] string[] TagNames,
			[FromQuery] bool MustHaveAllTagNames
		)
		{
			var queryFilter = new PieceQueryFilter(
				name, PurchaseDateMin, PurchaseDateMax, PriceMin, PriceMax, TagNames, MustHaveAllTagNames);
			return Ok(await PieceService.FilteredRowCountAsync(queryFilter));
		}

		[HttpPost("SaveFromFiles")]
		public async Task<ActionResult<IEnumerable<Piece>>> SaveFromFilesAsync([FromForm] IFormFileCollection files)
		{
			await PieceService.SaveFromFilesAsync(files, ContextTools.DefaultUserPath());
			return Ok();
		} 

		[HttpDelete("{id}")]
		public async Task<ActionResult> Remove(long id)
		{
			await PieceService.RemoveAsync(id);
			return Ok();
		}

		[HttpDelete("Multiple")]
		public async Task<ActionResult> RemoveMultiple([FromBody] long[] ids)
		{
			await PieceService.RemoveAsync(ids.ToList());
			return Ok(); //TODO: Improve returns
		}

		[HttpPut]
		public async Task<ActionResult> Update(PieceModel pieceModel)
		{
			await PieceService.UpdateAsync(pieceModel);
			return Ok();
		}

		[HttpPut("Multiple")]
		public async Task<ActionResult> UpdateMultiple(IEnumerable<PieceModel> pieceModels)
		{
			await PieceService.UpdateAsync(pieceModels);
			return Ok();
		}
	}
}
