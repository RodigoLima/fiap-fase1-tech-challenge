using fiap_fase1_tech_challenge.Database;
using fiap_fase1_tech_challenge.Database.Seeders;
using System;

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly IEnumerable<ISeeder> _seeders;

    public DatabaseSeeder(IEnumerable<ISeeder> seeders)
    {
        _seeders = seeders;
    }

    public void SeedDatabase(ApplicationContext context)
    {
        foreach (var seeder in _seeders)
        {
            seeder.Seed(context);

        }
    }

}

