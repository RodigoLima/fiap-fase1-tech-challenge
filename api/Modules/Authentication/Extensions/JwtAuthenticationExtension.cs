using fiap_fase1_tech_challenge.Modules.Authentication.Configurations;
using fiap_fase1_tech_challenge.Modules.Authentication.Messages;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace fiap_fase1_tech_challenge.Modules.Authentication.Extensions;

public static class JwtAuthenticationExtension
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceProvider = services.BuildServiceProvider();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("JwtAuthentication");

        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        var jwt = configuration.GetSection("Jwt").Get<JwtSettings>();
        if(jwt == null)
        {
            logger.LogError(JwtMessages.Configuration.NotConfigured);
            throw new InvalidOperationException(JwtMessages.Configuration.NotConfigured);
        };

        if (string.IsNullOrWhiteSpace(jwt.Key))
        {
            logger.LogError(JwtMessages.Configuration.InvalidKey);
            throw new InvalidOperationException(JwtMessages.Configuration.InvalidKey);
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = key
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Append("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}
