using Bogus;
using fiap_fase1_tech_challenge.Models;

namespace Tests.Utils
{
    public static class RoleFaker
    {
        public static List<Role> FakeListOfRoles(int qtToGenerate)
        {
            var roleFaker = new Faker<Role>()
                .RuleFor(u => u.Id, f => f.Random.Int(0, 3000))
                .RuleFor(u => u.CreatedAt, f => f.Date.Recent(30))
                .RuleFor(u => u.Name, f => f.Lorem.Word());
            return roleFaker.Generate(qtToGenerate);
        }
    }
}
