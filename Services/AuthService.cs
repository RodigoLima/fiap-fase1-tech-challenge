using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using fiap_fase1_tech_challenge.Configurations;
using fiap_fase1_tech_challenge.DTOs.Auth;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace fiap_fase1_tech_challenge.Services;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public AuthService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public TokenResponse GenerateTokens(string userId, string email, string role)
    {
        var accessToken = GenerateJwtToken(userId, email, role, TimeSpan.FromMinutes(_jwtSettings.AccessTokenExpirationMinutes));
        var refreshToken = GenerateJwtToken(userId, email, role, TimeSpan.FromDays(_jwtSettings.RefreshTokenExpirationDays), isRefresh: true);

        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var handler = _tokenHandler;

        var jwtToken = handler.ReadJwtToken(token);
        if (jwtToken.Header.Alg != SecurityAlgorithms.HmacSha256)
            return null;

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key))
        };

        try
        {
            return _tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
        }
        catch
        {
            return null;
        }
    }

    private string GenerateJwtToken(string userId, string email, string role, TimeSpan expiresIn, bool isRefresh = false)
    {
        var issuedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, issuedAt, ClaimValueTypes.Integer64)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        if (isRefresh)
        {
            claims.Add(new Claim("token_type", "refresh"));
        }

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(expiresIn),
            signingCredentials: creds
        );

        return _tokenHandler.WriteToken(token);
    }
}
