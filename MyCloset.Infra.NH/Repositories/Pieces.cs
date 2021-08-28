using Auth.Abstractions;
using Infra.NH;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Infra.Abstractions.Repositories;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util.Services;

namespace MyCloset.Infra.NH.Repositories
{
	public class Pieces : NHRepository<Piece>, IPieces
	{
		public Pieces(IUnitOfWork unitOfWork, IContextTools contextTools, IAccountProvider accountProvider) 
			: base(unitOfWork, contextTools, accountProvider)
		{
		}

		public async Task<IEnumerable<Piece>> FilteredAsync(IPieceQueryFilter queryFilter, int start, int quantity)
			=> await PagedAsync(queryFilter.ApplyFilters(Query()), start, quantity);

		async Task<IEnumerable<Piece>> PagedAsync(IQueryOver<Piece, Piece> query, int start, int quantity)
			=> await query.OrderBy(q => q.Creation).Desc().Skip(start).Take(quantity).ListAsync();

		public async Task<int> FilteredRowCountAsync(IPieceQueryFilter queryFilter)
			=> await queryFilter.ApplyFilters(Query()).Select(Projections.CountDistinct(Projections.Id())).SingleOrDefaultAsync<int>();
	}
}
