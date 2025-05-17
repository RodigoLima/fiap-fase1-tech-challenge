using Bogus;
using fiap_fase1_tech_challenge.Models;

namespace Tests.Utils
{
    public static class PromotionFaker
    {
        public static List<Promotion> FakeListOfPromotions(int qtToGenerate)
        {
            var promotionFaker = new Faker<Promotion>()
                .RuleFor(p => p.Id, f => f.Random.Int(0, 3000))
                .RuleFor(p => p.CreatedAt, f => f.Date.Recent(30))
                .RuleFor(p => p.DiscountPercentage, f => f.Random.Int(10, 90))
                .RuleFor(p => p.InitialDate, f => f.Date.Between(DateTime.Now, DateTime.Now.AddMonths(1)))
                .RuleFor(p => p.FinalDate, (f, p) => f.Date.Between(p.InitialDate, p.InitialDate.AddMonths(1)))
                .RuleFor(p => p.GameId, f => f.Random.Int(1, 3000));
            return promotionFaker.Generate(qtToGenerate);
        }
    }
}