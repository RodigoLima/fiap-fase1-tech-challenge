namespace fiap_fase1_tech_challenge.Messages
{
    public static class JwtMessages
    {
        public static class Authorization
        {
            public const string Forbidden = "O usuário não possui direitos suficientes para realizar esta operação.";
        }

        public static class Authentication
        {
            public const string InvalidToken = "O token JWT não foi enviado, é inválido ou está expirado.";
        }

        public static class Configuration
        {
            public const string NotConfigured = "A sessão 'JWT' não está configurada no appsettings da aplicação.";
            public const string InvalidKey = "A chave do Token JWT não está corretamente configurada no appsettings da aplicação.";
        }
    }
}
