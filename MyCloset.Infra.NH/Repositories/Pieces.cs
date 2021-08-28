using Auth.Abstractions;
using Infra.NH;
using MyCloset.Domain.Entities;
using MyCloset.Domain.QueryFilters;
using MyCloset.Infra.Abstractions.Repositories;
using MyCloset.Infra.NH.FilterResolvers;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util.Services;

namespace MyCloset.Infra.NH.Repositories
{
	public class Pieces : NHRepository<Piece>, IPieces
	{
		PieceFilterResolver FilterResolver { get; }
		public Pieces
		(
			IUnitOfWork unitOfWork, 
			IContextTools contextTools, 
			IAccountProvider accountProvider, 
			PieceFilterResolver pieceFilterResolver
		) : base(unitOfWork, contextTools, accountProvider)
		{
			FilterResolver = pieceFilterResolver;
		}

		public async Task<IEnumerable<Piece>> FilteredAsync(PieceQueryFilter filter, int start, int quantity)
			=> await PagedAsync(FilterResolver.ApplyFilters(Query(), filter), start, quantity);

		async Task<IEnumerable<Piece>> PagedAsync(IQueryOver<Piece, Piece> query, int start, int quantity)
			=> await query.OrderBy(q => q.Creation).Desc().Skip(start).Take(quantity).ListAsync();

		public async Task<int> FilteredRowCountAsync(PieceQueryFilter filter)
			=> await FilterResolver.ApplyFilters(Query(), filter).Select(Projections.CountDistinct(Projections.Id())).SingleOrDefaultAsync<int>();
	}
}
