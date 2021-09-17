using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCloset.Domain.Entities;
using MyCloset.Domain.Models;
using MyCloset.Domain.QueryFilters;
using MyCloset.Services.Abstractions.CrudServices;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Extensions;

namespace API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[Authorize]
	public class PieceController : ControllerBase
	{
		public PieceController(IPieceService pieceService)
		{
			PieceService = pieceService;
		}

		IPieceService PieceService { get; }

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
			await PieceService.SaveFromFilesAsync(files);

			return files.IsPlural()
				? Ok(string.Format(Resource.CreateMultiplePieceOk, files.Count()))
				: Ok(Resource.CreatePieceOk);
		} 

		[HttpDelete("Multiple")]
		public async Task<ActionResult> RemoveMultiple([FromBody] long[] ids)
		{
			await PieceService.RemoveAsync(ids.ToList());
			return ids.IsPlural()
				? Ok(string.Format(Resource.RemoveMultiplePieceOk, ids.Count()))
				: Ok(Resource.RemovePieceOk);
		}

		[HttpPut]
		public async Task<ActionResult> Update(PieceModel pieceModel)
		{
			await PieceService.UpdateAsync(pieceModel);
			return Ok(Resource.UpdatePieceOk);
		}
	}
}
