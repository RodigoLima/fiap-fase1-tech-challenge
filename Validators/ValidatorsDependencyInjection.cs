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

            return services;
        }
    }
}
