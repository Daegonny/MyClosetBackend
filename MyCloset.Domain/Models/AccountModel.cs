using Base.Domain;
using MyCloset.Domain.Entities;
using System;

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
	}
}
