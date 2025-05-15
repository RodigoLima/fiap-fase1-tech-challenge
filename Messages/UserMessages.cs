namespace fiap_fase1_tech_challenge.Messages
{
    public static class UserMessages
    {
        public static class General
        {
            public const string NotFound = "Usuário não encontrado.";
            public const string InvalidEmailOrPassword = "Email ou senha inválidos.";
        }

        public static class Name
        {
            public const string Required = "O nome é obrigatório.";
            public const string CannotBeEmpty = "O nome não pode ser branco.";

        }

        public static class Password
        {
            public const string Required = "A senha é obrigatória.";
            public const string InvalidLength = "A senha deve possuir pelo menos 8 caracteres.";
            public const string CannotBeEmpty = "A senha não pode ser branca.";
            public const string InvalidFormat = "A senha deve possuir letras maiúsculas, minúsculas, " +
                "números e ao menos um dos seguintes caracteres: !@#$%&*";

            public const string InvalidOld = "A senha anterior está inválida.";
            public const string CannotBeEmptyOld = "A senha anterior não pode ser branca.";
            public const string RequiredOld = "A senha anterior deve ser informada.";

        }

        public static class Email
        {
            public const string Required = "O email é obrigatório.";
            public const string CannotBeEmpty = "O email não pode ser branco.";
            public const string InvalidFormat = "O Email informado não está em um formato válido.";

        }

        public static class Role
        {
            public const string Required = "O papel é obrigatório.";
            public const string Invalid = "O papel informado está inválido.";
        }
    }
}
