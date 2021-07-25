using MyCloset.Domain.Entities;
using MyCloset.Infra.Map.Seed;

namespace MyCloset.Infra.Map.Entities
{
	public class AccountMap : EntityMap<Account>
	{
		public AccountMap()
		{
			Table("Account");
			Map(x => x.Email);
			Map(x => x.Password).LazyLoad();
			Map(x => x.HashedFilePath).Column("hashed_file_path");
		}
	}
}
