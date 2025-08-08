using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Toodo.Application.Common.Behaviours;

namespace Toodo.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Register MediatR
        services.AddMediatR(config => config
            .RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            .AddOpenBehavior(typeof(ValidationBehaviour<,>))
        );

        // Register FluentValidation configurations
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Register AutoMapper
        services.AddAutoMapper(_ => { }, Assembly.GetExecutingAssembly());
        
    }
}