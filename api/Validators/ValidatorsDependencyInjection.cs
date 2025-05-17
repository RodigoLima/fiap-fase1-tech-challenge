using fiap_fase1_tech_challenge.DTOs.Game;
using fiap_fase1_tech_challenge.DTOs.Promotion;
using fiap_fase1_tech_challenge.DTOs.Role;
using fiap_fase1_tech_challenge.Validators.Game;
using fiap_fase1_tech_challenge.Validators.Promotion;
using fiap_fase1_tech_challenge.Validators.Role;
using fiap_fase1_tech_challenge.Validators.User;
using FluentValidation;

namespace fiap_fase1_tech_challenge.Validators
{
    public static class ValidatorsDependencyInjection
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserCreateRequest>, UserCreateRequestValidator>();
            services.AddScoped<IValidator<UserUpdateRequest>, UserUpdateRequestValidator>();

            services.AddScoped<IValidator<GameCreateRequest>, GameCreateRequestValidator>();
            services.AddScoped<IValidator<GameUpdateRequest>, GameUpdateRequestValidator>();

            services.AddScoped<IValidator<RoleCreateRequest>, RoleCreateRequestValidator>();
            services.AddScoped<IValidator<RoleUpdateRequest>, RoleUpdateRequestValidator>();

            services.AddScoped<IValidator<PromotionCreateRequest>, PromotionCreateRequestValidator>();
            services.AddScoped<IValidator<PromotionUpdateRequest>, PromotionUpdateRequestValidator>();

            return services;
        }
    }
}
