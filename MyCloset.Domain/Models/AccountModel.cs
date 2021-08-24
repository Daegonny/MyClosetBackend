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

		public Account Update(Account entity, string secret)
		{
			entity.Name = Name;
			entity.Email = Email;
			entity.Password = Password.Encrypt(entity.Creation, secret);
			return entity;
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
