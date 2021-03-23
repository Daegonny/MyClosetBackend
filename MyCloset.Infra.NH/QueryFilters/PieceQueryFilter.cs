using Infra.Abstractions;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Infra.NH.QueryFilters.Seed;
using NHibernate;
using System;
using System.Collections.Generic;
using Util.Extensions;
using System.Linq;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace MyCloset.Infra.NH.QueryFilters
{
	public class PieceQueryFilter : EntityQueryFilter<Piece>, IQueryFilter<Piece>, IPieceQueryFilter
	{
		public DateTime? PurchaseDateMin { get; set; }
		public DateTime? PurchaseDateMax { get; set; }
		public decimal? PriceMin { get; }
		public decimal? PriceMax { get; }
		public IEnumerable<string> TagNames { get; }
		public bool MustHaveAllTagNames { get; }

		Tag tagAlias = null;
		Piece Alias = null;

		public PieceQueryFilter(
			string name, 
			DateTime? purchaseDateMin,
			DateTime? purchaseDateMax,
			decimal? priceMin,
			decimal? priceMax,
			IEnumerable<string> tagNames,
			bool mustHaveAllTagNames
		) : base(name)
		{
			PurchaseDateMin = purchaseDateMin;
			PurchaseDateMax = purchaseDateMax;
			PriceMin = priceMin;
			PriceMax = priceMax;
			MustHaveAllTagNames = mustHaveAllTagNames;
			TagNames = tagNames.Select( t => t.Clean());
		}

		public PieceQueryFilter(){}

		new public IQueryOver<Piece, Piece> ApplyFilters(IQueryOver<Piece, Piece> query)
		{
			var queryFilter = base.ApplyFilters(query);

			if (PurchaseDateMin.NotNull())
				queryFilter = ApplyFilterPurchaseDateMin(queryFilter);

			if (PurchaseDateMax.NotNull())
				queryFilter = ApplyFilterPurchaseDateMax(queryFilter);

			if (PriceMin.NotNull())
				queryFilter = ApplyFilterPriceMin(queryFilter);

			if (PriceMax.NotNull())
				queryFilter = ApplyFilterPriceMax(queryFilter);

			if (!TagNames.IsNullOrEmpty())
				queryFilter = ApplyFilterTagNames(queryFilter);

			return queryFilter;
		}

		private IQueryOver<Piece, Piece> ApplyFilterPurchaseDateMin(IQueryOver<Piece, Piece> queryFilter) 
			=> queryFilter.Where(q => q.Purchase >= PurchaseDateMin);

		private IQueryOver<Piece, Piece> ApplyFilterPurchaseDateMax(IQueryOver<Piece, Piece> queryFilter)
			=> queryFilter.Where(q => q.Purchase >= PurchaseDateMax);

		private IQueryOver<Piece, Piece> ApplyFilterPriceMin(IQueryOver<Piece, Piece> queryFilter)
			=> queryFilter.Where(q => q.Price >= PriceMin);

		private IQueryOver<Piece, Piece> ApplyFilterPriceMax(IQueryOver<Piece, Piece> queryFilter)
			=> queryFilter.Where(q => q.Price >= PriceMax);

		private IQueryOver<Piece, Piece> ApplyFilterTagNames(IQueryOver<Piece, Piece> queryFilter)
			=> MustHaveAllTagNames ? ApplyFilterTagNamesMustHaveAll(queryFilter) : ApplyFilterTagNamesMustHaveAny(queryFilter);

		private IQueryOver<Piece, Piece> ApplyFilterTagNamesMustHaveAny(IQueryOver<Piece, Piece> queryFilter)
			=> queryFilter.JoinAlias(() => Alias.Tags, () => tagAlias).WhereRestrictionOn(() => tagAlias.Name).IsInG(TagNames)
				.Select(Projections.Distinct(Projections.RootEntity()));

		private IQueryOver<Piece, Piece> ApplyFilterTagNamesMustHaveAll(IQueryOver<Piece, Piece> queryFilter)  //TODO: Improve performance	
			=> queryFilter.WithSubquery.WhereProperty(() => Alias.Id).In(SubQueryTagNamesMustHaveAll());

		private QueryOver<Piece, Piece> SubQueryTagNamesMustHaveAll() 
			=> QueryOver.Of<Piece>(() => Alias).JoinAlias(() => Alias.Tags, () => tagAlias)
				.WhereRestrictionOn(() => tagAlias.Name).IsInG(TagNames)
				.Select(Projections.GroupProperty(Projections.Property<Piece>(p => p.Id)))
				.Where(Restrictions.Eq(Projections.CountDistinct(() => tagAlias.Id), TagNames.Count()));

	}
}
