using Infra.Abstractions;
using MyCloset.Domain.Entities;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Infra.NH.QueryFilters.Seed;

namespace MyCloset.Infra.NH.QueryFilters
{
	public class UserQueryFilter : EntityQueryFilter<User>, IQueryFilter<User>, IUserQueryFilter {}
}
