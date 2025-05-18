namespace fiap_fase1_tech_challenge.Modules.Authentication.DTOs.Responses
{
    public class TokenResponse
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
