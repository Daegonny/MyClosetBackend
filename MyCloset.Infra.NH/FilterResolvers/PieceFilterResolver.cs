using MyCloset.Domain.Entities;
using MyCloset.Domain.QueryFilters;
using NHibernate;
using NHibernate.Criterion;
using System.Linq;
using Util.Extensions;

namespace MyCloset.Infra.NH.FilterResolvers
{
	class PieceFilterResolver
	{

		Tag tagAlias = null;
		Piece Alias = null;

		public IQueryOver<Piece, Piece> ApplyFilters(IQueryOver<Piece, Piece> query, PieceQueryFilter filter)
		{
			if (filter.Name.NotNull() && filter.Name.Trim().Length > 0)
				query = query.WhereRestrictionOn(q => q.Name).IsInsensitiveLike($"%{filter.Name}%");

			if (filter.PurchaseDateMin.NotNull())
				query = ApplyFilterPurchaseDateMin(query, filter);

			if (filter.PurchaseDateMax.NotNull())
				query = ApplyFilterPurchaseDateMax(query, filter);

			if (filter.PriceMin.NotNull())
				query = ApplyFilterPriceMin(query, filter);

			if (filter.PriceMax.NotNull())
				query = ApplyFilterPriceMax(query, filter);

			if (!filter.TagNames.IsNullOrEmpty())
				query = ApplyFilterTagNames(query, filter);

			return query;
		}

		private IQueryOver<Piece, Piece> ApplyFilterPurchaseDateMin(IQueryOver<Piece, Piece> query, PieceQueryFilter filter)
			=> query.Where(q => q.Purchase >= filter.PurchaseDateMin);

		private IQueryOver<Piece, Piece> ApplyFilterPurchaseDateMax(IQueryOver<Piece, Piece> query, PieceQueryFilter filter)
			=> query.Where(q => q.Purchase >= filter.PurchaseDateMax);

		private IQueryOver<Piece, Piece> ApplyFilterPriceMin(IQueryOver<Piece, Piece> query, PieceQueryFilter filter)
			=> query.Where(q => q.Price >= filter.PriceMin);

		private IQueryOver<Piece, Piece> ApplyFilterPriceMax(IQueryOver<Piece, Piece> query, PieceQueryFilter filter)
			=> query.Where(q => q.Price >= filter.PriceMax);

		private IQueryOver<Piece, Piece> ApplyFilterTagNames(IQueryOver<Piece, Piece> query, PieceQueryFilter filter)
			=> filter.MustHaveAllTagNames 
				? ApplyFilterTagNamesMustHaveAll(query, filter) 
				: ApplyFilterTagNamesMustHaveAny(query, filter);

		private IQueryOver<Piece, Piece> ApplyFilterTagNamesMustHaveAny(IQueryOver<Piece, Piece> query, PieceQueryFilter filter)
			=> query.JoinAlias(() => Alias.Tags, () => tagAlias).WhereRestrictionOn(() => tagAlias.Name).IsInG(filter.TagNames)
				.Select(Projections.Distinct(Projections.RootEntity()));

		private IQueryOver<Piece, Piece> ApplyFilterTagNamesMustHaveAll(IQueryOver<Piece, Piece> query, PieceQueryFilter filter)  //TODO: Improve performance	
			=> query.WithSubquery.WhereProperty(() => Alias.Id).In(SubQueryTagNamesMustHaveAll(filter));

		private QueryOver<Piece, Piece> SubQueryTagNamesMustHaveAll(PieceQueryFilter filter)
			=> QueryOver.Of<Piece>(() => Alias).JoinAlias(() => Alias.Tags, () => tagAlias)
				.WhereRestrictionOn(() => tagAlias.Name).IsInG(filter.TagNames)
				.Select(Projections.GroupProperty(Projections.Property<Piece>(p => p.Id)))
				.Where(Restrictions.Eq(Projections.CountDistinct(() => tagAlias.Id), filter.TagNames.Count()));
	}
}
