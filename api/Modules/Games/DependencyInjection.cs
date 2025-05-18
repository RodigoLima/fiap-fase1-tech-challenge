using fiap_fase1_tech_challenge.Modules.Games.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Games.Repositories.Implementations;
using fiap_fase1_tech_challenge.Modules.Games.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Modules.Games.Services.Implementations;
using fiap_fase1_tech_challenge.Modules.Games.Validators;
using FluentValidation;

namespace fiap_fase1_tech_challenge.Modules.Games
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGamesModule(this IServiceCollection services)
        {
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGameService, GameService>();

            services.AddScoped<IValidator<GameCreateRequest>, GameCreateRequestValidator>();
            services.AddScoped<IValidator<GameUpdateRequest>, GameUpdateRequestValidator>();

            return services;
        }
    }
}
