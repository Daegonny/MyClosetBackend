using Infra.Abstractions;
using MyCloset.Domain.Entities;
using MyCloset.Domain.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCloset.Infra.Abstractions.Repositories
{
	public interface IPieces : IRepository<Piece>
	{
		Task<IEnumerable<Piece>> FilteredAsync(PieceQueryFilter filter, int start, int quantity);
		Task<int> FilteredRowCountAsync(PieceQueryFilter filter);
	}
}
