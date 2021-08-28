using Infra.Abstractions;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.QueryFilters;

namespace MyCloset.Infra.Abstractions.Repositories
{
	public interface IPieces : IRepository<Piece>, 
		IFiltrableRepository<Piece, IPieceQueryFilter>
	{
	}
}
