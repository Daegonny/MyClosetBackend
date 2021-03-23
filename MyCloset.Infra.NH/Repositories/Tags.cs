using Infra.NH;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Infra.Abstractions.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util.Services;

namespace MyCloset.Infra.NH.Repositories
{
	public class Tags : NHRepository<Tag, ITagQueryFilter>, ITags
	{
		public Tags(IUnitOfWork unitOfWork, IContextTools contextTools) : base(unitOfWork, contextTools)
		{
		}

		public async Task<IEnumerable<Tag>> ByNamesAsync(IEnumerable<string> tagNames)
			=> await Query().WhereRestrictionOn(t => t.Name).IsInG(tagNames).ListAsync();
	}
}
