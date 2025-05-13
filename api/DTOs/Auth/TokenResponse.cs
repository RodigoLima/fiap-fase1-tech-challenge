namespace fiap_fase1_tech_challenge.DTOs.Auth
{
    public class TokenResponse
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
