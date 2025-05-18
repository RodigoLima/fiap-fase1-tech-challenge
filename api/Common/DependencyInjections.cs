using fiap_fase1_tech_challenge.Common.Extensions;
using fiap_fase1_tech_challenge.Common.Services.Implementations;
using fiap_fase1_tech_challenge.Common.Services.Interfaces;

namespace fiap_fase1_tech_challenge.Common
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddScoped<IHasher, Hasher>();
            services.AddSwaggerWithJwt();

            return services;
        }
    }
}
