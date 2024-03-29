﻿using System;
using System.Collections.Generic;
using Util.Extensions;
using System.Linq;
using Exceptions.BadRequest;
using Resources;

namespace MyCloset.Domain.QueryFilters
{
	public class PieceQueryFilter
	{
		public string Name { get; set; }
		public DateTime? PurchaseDateMin { get; set; }
		public DateTime? PurchaseDateMax { get; set; }
		public decimal? PriceMin { get; }
		public decimal? PriceMax { get; }
		public IEnumerable<string> TagNames { get; }
		public bool MustHaveAllTagNames { get; }

		public PieceQueryFilter(
			string name, 
			DateTime? purchaseDateMin,
			DateTime? purchaseDateMax,
			decimal? priceMin,
			decimal? priceMax,
			IEnumerable<string> tagNames,
			bool mustHaveAllTagNames
		)
		{
			Name = name;
			PurchaseDateMin = purchaseDateMin;
			PurchaseDateMax = purchaseDateMax;
			PriceMin = priceMin;
			PriceMax = priceMax;
			MustHaveAllTagNames = mustHaveAllTagNames;
			TagNames = tagNames.Select( t => t.Clean());
		}

		public PieceQueryFilter(){}

		public PieceQueryFilter Validate()
		{
			if (TagNames.Count() > 10)
				throw new BadRequestException(string.Format(Resource.OverLimitError, Resource.TagsField, 10));

			return this;
		}
	}
}
