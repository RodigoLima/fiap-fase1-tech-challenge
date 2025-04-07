using fiap_fase1_tech_challenge.Database.Seeders;
using System;

namespace fiap_fase1_tech_challenge.Database
{
    public class DatabaseSeeder: IDatabaseSeeder
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

}
