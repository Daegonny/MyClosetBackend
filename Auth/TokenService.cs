using Auth.Abstractions;
using Microsoft.IdentityModel.Tokens;
using MyCloset.Domain.Entities;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Util.Services;

namespace Auth
{
	public class TokenService : ITokenService
	{
		ITokenConfig TokenConfig { get; }
		IContextTools ContextTools { get; }

		public TokenService(ITokenConfig tokenConfig, IContextTools contextTools)
		{
			TokenConfig = tokenConfig;
			ContextTools = contextTools;
		}

		public User AddTokenTo(User user) 
			=> user.AddToken(new JwtSecurityTokenHandler().WriteToken(GetJwtToken(GetClaims(user), GetCredentials())));

		private IEnumerable<Claim> GetClaims(User user)
			=> new[]
			{
				new Claim("Id", user.Id.ToString()),
				new Claim("Email", user.Email),
				new Claim("Name", user.Name),
				new Claim("Path", user.Name),
			};

		private SigningCredentials GetCredentials()
		{
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConfig.SecretGUID));
			return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		}

		private JwtSecurityToken GetJwtToken(IEnumerable<Claim> claims, SigningCredentials credentials)
			=> new JwtSecurityToken(
				TokenConfig.Issuer,
				TokenConfig.Audience,
				claims,
				expires: ContextTools.Now().AddSeconds(TokenConfig.ExpirationTimeInSeconds),
				signingCredentials: credentials);
	}
}
