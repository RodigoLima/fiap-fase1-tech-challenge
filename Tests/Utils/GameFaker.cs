﻿using Bogus;
using fiap_fase1_tech_challenge.Modules.Games.Models;

namespace fiap_fase1_tech_challenge.Test.Utils
{
    public static class GameFaker
    {
        public static List<Game> FakeListOfGame(int qtToGenerate)
        {
            var gameFaker = new Faker<Game>()
                .RuleFor(g => g.Id, f => f.Random.Int(0, 3000))
                .RuleFor(g => g.CreatedAt, f => f.Date.Recent(30))
                .RuleFor(g => g.Name, f => f.Commerce.ProductName())
                .RuleFor(g => g.Description, f =>
                {
                    var text = f.Lorem.Paragraphs(3);
                    if (text.Length > 500)
                    {
                        text = text.Substring(0, 500);
                        var lastSpace = text.LastIndexOf(' ');
                        if (lastSpace > 0)
                            text = text.Substring(0, lastSpace);
                    }
                    return text;
                })
                .RuleFor(g => g.Price, f => f.Random.Double(100, 500))
                .RuleFor(g => g.ReleasedDate, f => f.Date.Recent(30))
                .RuleFor(g => g.Genre, f => f.Lorem.Word());
            return gameFaker.Generate(qtToGenerate);
        }


    }
}
