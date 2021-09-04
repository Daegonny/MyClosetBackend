using Microsoft.AspNetCore.Http;
using MyCloset.Domain.Entities;
using MyCloset.Domain.Models;
using MyCloset.Domain.QueryFilters;
using MyCloset.Services.Abstractions.Seed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCloset.Services.Abstractions.CrudServices
{
	public interface IPieceService : ICrudService<Piece>
	{
		Task<IEnumerable<Piece>> FilteredAsync(PieceQueryFilter queryFilter, int start, int quantity);
		Task<int> FilteredRowCountAsync(PieceQueryFilter queryFilter);
		Task RemoveAsync(IEnumerable<long> ids);
		Task SaveFromFilesAsync(IFormFileCollection files);
		Task UpdateAsync(PieceModel pieceModel);
		Task UpdateAsync(IEnumerable<PieceModel> pieceModels);
	}
}
