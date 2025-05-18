namespace fiap_fase1_tech_challenge.Modules.Games.Messages
{
    public static class GameMessages
    {
        public static class General
        {
            public const string NotFound = "Jogo não encontrado.";
        }

        public static class Name
        {
            public const string Required = "O nome do jogo deve ser informado.";
            public const string MaxLength = "O nome do jogo deve ter no máximo 50 caracteres.";
        }

        public static class Description
        {
            public const string MaxLength = "A descrição do jogo deve ter no máximo 500 caracteres.";
        }

        public static class Price
        {
            public const string GreaterThanZero = "O preço do jogo deve ser maior que zero.";
        }

        public static class Genre
        {
            public const string Required = "O gênero do jogo deve ser informado.";
        }
    }
}
