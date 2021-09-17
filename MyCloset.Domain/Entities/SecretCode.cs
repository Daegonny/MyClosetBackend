using Base.Domain;
using Exceptions.Auth;
using MyCloset.Domain.Enums;
using System;
using Util.Extensions;

namespace MyCloset.Domain.Entities
{
	public class SecretCode : Entity, IHaveOwner
	{
		public virtual SecretCodeType Type { get; set; }
		public virtual DateTime? Activation { get; set; }
		public virtual DateTime? Expiration { get; set; }
		public virtual Account Account { get; set; }
		public SecretCode(){}

		public virtual bool AssertIsOwnedBy(Account account)
		{
			if (Account.Id != account.Id)
				throw new AccessDeniedException();
			return true;
		}

		public virtual bool IsExpired(DateTime today) 
			=> today > Expiration;

		public virtual bool IsConsumed()
			=> Activation.NotNull();
	}
}
