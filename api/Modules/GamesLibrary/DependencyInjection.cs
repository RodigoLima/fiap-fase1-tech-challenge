using fiap_fase1_tech_challenge.Modules.GamesLibrary.Repositories.Implementations;
using fiap_fase1_tech_challenge.Modules.GamesLibrary.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Modules.GamesLibrary.Services.Implementations;
using fiap_fase1_tech_challenge.Modules.GamesLibrary.Services.Interfaces;

namespace fiap_fase1_tech_challenge.Modules.GamesLibrary
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGamesLibraryModule(this IServiceCollection services)
        {
            services.AddScoped<IGameLibraryRepository, GameLibraryRepository>();
            services.AddScoped<IGameLibraryService, GameLibraryService>();

            return services;
        }
    }
}
