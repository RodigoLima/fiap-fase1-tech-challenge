using fiap_fase1_tech_challenge.Modules.Users.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Users.Repositories.Implementations;
using fiap_fase1_tech_challenge.Modules.Users.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Modules.Users.Services.Implementations;
using fiap_fase1_tech_challenge.Modules.Users.Services.Interfaces;
using fiap_fase1_tech_challenge.Modules.Users.Validators;
using FluentValidation;

namespace fiap_fase1_tech_challenge.Modules.Users
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUsersModule(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IValidator<UserCreateRequest>, UserCreateRequestValidator>();
            services.AddScoped<IValidator<UserUpdateRequest>, UserUpdateRequestValidator>();

            services.AddScoped<IValidator<RoleCreateRequest>, RoleCreateRequestValidator>();
            services.AddScoped<IValidator<RoleUpdateRequest>, RoleUpdateRequestValidator>();

            return services;
        }
    }
}
