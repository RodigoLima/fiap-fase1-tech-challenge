using fiap_fase1_tech_challenge.Database.Seeders;
using fiap_fase1_tech_challenge.Database;
using fiap_fase1_tech_challenge.Repositories;
using fiap_fase1_tech_challenge.Services;

namespace fiap_fase1_tech_challenge.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISeeder, RoleSeeder>();
            services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();
            services.AddScoped<AuthService>();

            return services;
        }
    }
}
