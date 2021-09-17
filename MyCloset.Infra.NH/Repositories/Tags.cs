using Auth.Abstractions;
using Infra.NH;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util.Services;

namespace MyCloset.Infra.NH.Repositories
{
	public class Tags : NHRepository<Tag>, ITags
	{
		public Tags(IUnitOfWork unitOfWork, IContextTools contextTools, IAccountProvider accountProvider) 
			: base(unitOfWork, contextTools, accountProvider)
		{
		}

		public async Task<IEnumerable<Tag>> ByNamesAsync(IEnumerable<string> tagNames)
			=> await QueryFilteringOwner().WhereRestrictionOn(t => t.Name).IsInG(tagNames).ListAsync();
	}
}
