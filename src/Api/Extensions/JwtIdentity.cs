namespace Api.Extensions;

public class JwtIdentity
{
    public int ExpirationInHours { get; set; }
    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}