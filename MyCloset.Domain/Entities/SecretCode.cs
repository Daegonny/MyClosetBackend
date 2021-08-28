﻿using Exceptions.Auth;
using MyCloset.Domain.Enums;
using System;

namespace MyCloset.Domain.Entities
{
	public class SecretCode : IHaveOwner
	{
		public virtual long Id { get; set; }
		public virtual string Value { get; set; }
		public virtual SecretCodeType Type { get; set; }
		public virtual DateTime Activation { get; set; }
		public virtual DateTime Expiration { get; set; }
		public virtual Account Account { get; set; }
		public SecretCode(){}

		public bool AssertIsOwnedBy(Account account)
		{
			if (Account.Id != account.Id)
				throw new AccessDeniedException();
			return true;
		}
	}
}
