﻿using MyCloset.Domain.Entities;
using MyCloset.Domain.Models;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Services.Abstractions.Seed;
using System.Threading.Tasks;

namespace MyCloset.Services.Abstractions.CrudServices
{
	public interface IAccountService : ICrudService<Account, IAccountQueryFilter>
	{
		Task SaveAsync(AccountModel model);
		Task UpdateAsync(AccountModel model);
	}
}