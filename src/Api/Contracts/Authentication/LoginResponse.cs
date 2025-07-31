namespace Api.Contracts.Authentication;

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public double ExpiresIn { get; set; }
}