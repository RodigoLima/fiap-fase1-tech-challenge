namespace fiap_fase1_tech_challenge.Modules.Authentication.Configurations
{
    public class JwtSettings
    {
        public required string Key { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required int AccessTokenExpirationMinutes { get; set; }
        public required int RefreshTokenExpirationDays { get; set; }
    }
}
