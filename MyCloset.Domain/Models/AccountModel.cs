using Base.Domain;
using MyCloset.Domain.Entities;
using System;
using Util.Extensions;

namespace MyCloset.Domain.Models
{
	public class AccountModel : EntityModel<Account>
	{
		public string Email { get; set; }
		public string EmailConfirm { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }
		public string SecretCode { get; set; }

		public override Account Update(Account entity)
		{
			throw new NotImplementedException();
		}

		public Account ToEntity(DateTime creation, string secret)
		{
			return new Account()
			{
				Name = Name,
				Email = Email,
				Creation = creation,
				Password = Password.Encrypt(creation, secret),
				HashedFilePath = Email.Encrypt(creation, secret)
			};
		}
	}
}
