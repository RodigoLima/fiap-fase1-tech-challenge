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

            // Adicione outras dependências aqui...

            return services;
        }
    }
}
