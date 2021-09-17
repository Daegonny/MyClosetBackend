using Auth.Abstractions;

namespace Auth
{
	public class TokenConfig : ITokenConfig
	{
		public long ExpirationTimeInSeconds { get; }
		public string SecretGUID { get; }
		public string Issuer { get; }
		public string Audience { get; }
		public TokenConfig(long expirationTimeInSeconds, string secretGUID, string issuer, string audience)
		{
			ExpirationTimeInSeconds = expirationTimeInSeconds;
			SecretGUID = secretGUID;
			Issuer = issuer;
			Audience = audience;
		}
	}
}
