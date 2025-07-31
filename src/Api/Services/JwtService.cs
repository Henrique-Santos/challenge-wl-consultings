using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Api.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class JwtService : IJwtService
{
    private readonly JwtIdentity _jwtIdentity;

    public JwtService(IOptions<JwtIdentity> jwtIdentity)
    {
        _jwtIdentity = jwtIdentity.Value;
    }

    public string GenerateJwtToken()
    {
        var token = CreateToken();
        return token;
    }

    private string CreateToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtIdentity.SecretKey);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtIdentity.Issuer,
            Audience = _jwtIdentity.Audience,
            Expires = DateTime.UtcNow.AddHours(_jwtIdentity.ExpirationInHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });
        var encodedToken = tokenHandler.WriteToken(token);
        return encodedToken;
    }
}