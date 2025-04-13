using fiap_fase1_tech_challenge.Database.Seeders;
using fiap_fase1_tech_challenge.Database;
using fiap_fase1_tech_challenge.Repositories;
using fiap_fase1_tech_challenge.Services;
using fiap_fase1_tech_challenge.Services.Interfaces;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

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
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<IGameLibraryRepository, GameLibraryRepository>();
            services.AddScoped<ISeeder, RoleSeeder>();
            services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();
            services.AddScoped<AuthService>();

            return services;
        }

        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // FallbackPolicy: toda rota exige autenticação por padrão
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            return services;
        }
    }
}
