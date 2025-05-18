using fiap_fase1_tech_challenge.Modules.Promotions.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Promotions.Repositories.Implementations;
using fiap_fase1_tech_challenge.Modules.Promotions.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Modules.Promotions.Services.Implementations;
using fiap_fase1_tech_challenge.Modules.Promotions.Services.Interfaces;
using fiap_fase1_tech_challenge.Modules.Promotions.Validators;
using FluentValidation;

namespace fiap_fase1_tech_challenge.Modules.Promotions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPromotionsModule(this IServiceCollection services)
        {
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<IPromotionService, PromotionService>();

            services.AddScoped<IValidator<PromotionCreateRequest>, PromotionCreateRequestValidator>();
            services.AddScoped<IValidator<PromotionUpdateRequest>, PromotionUpdateRequestValidator>();

            return services;
        }
    }
}
