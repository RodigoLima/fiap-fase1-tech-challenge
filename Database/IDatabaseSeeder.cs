using System;

namespace fiap_fase1_tech_challenge.Database
{
    public interface IDatabaseSeeder
    {
        void SeedDatabase(ApplicationContext context);
    }
}
