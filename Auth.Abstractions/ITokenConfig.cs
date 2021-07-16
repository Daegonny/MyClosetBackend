namespace Auth.Abstractions
{
	public interface ITokenConfig
	{
		long ExpirationTimeInSeconds { get; }
		string SecretGUID { get; }
		string Issuer { get; }
		string Audience { get; }
	}
}
