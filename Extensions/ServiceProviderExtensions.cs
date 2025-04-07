using fiap_fase1_tech_challenge.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace fiap_fase1_tech_challenge.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ApplyMigrationsAndSeed(this IServiceCollection services)
        {
           
            using var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var seeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();

            context.Database.Migrate();
            seeder.SeedDatabase(context); 

            return services;
        }
    }
}
