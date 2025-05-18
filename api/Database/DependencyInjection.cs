using fiap_fase1_tech_challenge.Database.Seeders;
using Microsoft.EntityFrameworkCore;

namespace fiap_fase1_tech_challenge.Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
        {
            services.AddScoped<ISeeder, RoleSeeder>();
            services.AddScoped<ISeeder, UserSeeder>();
            services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();
            services.ApplyMigrationsAndSeed();

            return services;
        }

        private static IServiceCollection ApplyMigrationsAndSeed(this IServiceCollection services)
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
