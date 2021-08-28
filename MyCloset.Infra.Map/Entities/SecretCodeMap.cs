using MyCloset.Domain.Entities;
using MyCloset.Domain.Enums;
using MyCloset.Infra.Map.Seed;

namespace MyCloset.Infra.Map.Entities
{
	public class SecretCodeMap : EntityMap<SecretCode>
	{
		public SecretCodeMap() : base()
		{
			Map(x => x.Type).CustomType<SecretCodeType>(); ;
			Map(x => x.Activation);
			Map(x => x.Expiration);
			References(x => x.Account);
		}
	}
}
