using FluentNHibernate.Mapping;
using MyCloset.Domain.Entities;
using MyCloset.Domain.Enums;

namespace MyCloset.Infra.Map.Entities
{
	public class SecretCodeMap : ClassMap<SecretCode>
	{
		public SecretCodeMap()
		{
			Id(x => x.Id).GeneratedBy.Identity();
			Map(x => x.Value);
			Map(x => x.Type).CustomType<SecretCodeType>(); ;
			Map(x => x.Activation);
			Map(x => x.Expiration);
			References(x => x.Account);
		}
	}
}
