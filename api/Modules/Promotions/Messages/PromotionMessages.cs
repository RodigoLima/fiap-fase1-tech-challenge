namespace fiap_fase1_tech_challenge.Modules.Promotions.Messages
{
    public static class PromotionMessages
    {
        public static class General
        {
            public const string NotFound = "Promoção não encontrada.";
        }

        public static class Discount
        {
            public const string GreatherThan = "O percentual do desconto deve ser maior que 0.";
            public const string LessThan = "O percentual do desconto não deve ultrapassar 100%.";
        }

        public static class InitialDate
        {
            public const string GreaterOrEqual = "A data inicial não pode ser menor que a data atual.";
            public const string Required = "A data inicial deve ser informada.";
        }

        public static class FinalDate
        {
            public const string GreaterThan = "A data final deve ser maior que a data inicial.";
            public const string Required = "A data final deve ser informada.";
        }

        public static class Game
        {
            public const string Required = "O id do jogo é obrigatório.";
        }
    }
}
