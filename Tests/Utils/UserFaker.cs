using Bogus;
using fiap_fase1_tech_challenge.Modules.Users.Models;

namespace fiap_fase1_tech_challenge.Test.Utils
{
    public static class UserFaker
    {
        public static List<User> FakeListOfUser(int qtToGenerate)
        {
            var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => f.Random.Int(0,3000))
                .RuleFor(u => u.CreatedAt, f => f.Date.Recent(30))
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => CreatePassword(8))
                .RuleFor(u => u.RoleId, f => f.Random.Int(1, 2));
            return userFaker.Generate(qtToGenerate);
        }

        private static string CreatePassword(int size)
        {
            var rand = new Random();
            const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string specialCharacters = "!@#$%&*";

            var password = new[]
            {
                upperCase[rand.Next(upperCase.Length)],
                lowerCase[rand.Next(lowerCase.Length)],
                numbers[rand.Next(numbers.Length)],
                specialCharacters[rand.Next(specialCharacters.Length)]
            }.ToList();

            var allCharacteres = upperCase + lowerCase + numbers + specialCharacters;
            password.AddRange(Enumerable.Range(0, size - password.Count)
                .Select(_ => allCharacteres[rand.Next(allCharacteres.Length)]));

            return new string(password.OrderBy(_ => rand.Next()).ToArray());

        }
    }
}