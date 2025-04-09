using fiap_fase1_tech_challenge.DTOs.Auth;
using System.Security.Claims;

namespace fiap_fase1_tech_challenge.Repositories.Interfaces
{
    public interface IAuthService
    {
        TokenResponse GenerateTokens(string userId, string email, string role);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }

}
