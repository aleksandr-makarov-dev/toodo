using Microsoft.OpenApi.Models;
using Toodo.API.Infrastructure;
using Toodo.API.Services;
using Toodo.Application.Common.Security;

namespace Toodo.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPresentationServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen((options) =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Toodo API", Version = "v1" });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter a valid authorization token.",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });

        services.AddExceptionHandler<ExceptionHandler>();

        services.AddScoped<IUserContext, UserContext>();
    }
}