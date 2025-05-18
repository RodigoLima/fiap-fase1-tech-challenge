using fiap_fase1_tech_challenge.Modules.Authentication.Extensions;
using fiap_fase1_tech_challenge.Modules.Authentication.Handlers;
using fiap_fase1_tech_challenge.Modules.Authentication.Services.Implementations;
using fiap_fase1_tech_challenge.Modules.Authentication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace fiap_fase1_tech_challenge.Modules.Authentication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthenticationModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();

            services.AddAuthorization(options =>
            {
                // FallbackPolicy: toda rota exige autenticação por padrão
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.AddPolicy("Admin", p => p.RequireRole("Admin"));
            });

            services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationResultHandler>();

            services.AddJwtAuthentication(configuration);

            return services;
        }
    }
}
