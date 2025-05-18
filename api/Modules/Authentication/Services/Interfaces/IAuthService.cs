using fiap_fase1_tech_challenge.Modules.Authentication.DTOs.Responses;
using System.Security.Claims;

namespace fiap_fase1_tech_challenge.Modules.Authentication.Services.Interfaces
{
    public interface IAuthService
    {
        TokenResponse GenerateTokens(string userId, string email, string role);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }

}
