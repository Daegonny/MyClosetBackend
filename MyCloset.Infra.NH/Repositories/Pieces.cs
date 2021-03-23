using Infra.NH;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Infra.Abstractions.Repositories;
using Util.Services;

namespace MyCloset.Infra.NH.Repositories
{
	public class Pieces : NHRepository<Piece, IPieceQueryFilter>, IPieces
	{
		public Pieces(IUnitOfWork unitOfWork, IContextTools contextTools) : base(unitOfWork, contextTools)
		{
		}
	}
}
