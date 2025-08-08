using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Toodo.Application.Common.Behaviours;
using Toodo.Application.Common.Security;
using Toodo.Application.Emails;
using Toodo.Application.Issues;
using Toodo.Infrastructure.Identity;

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

        // Register IEmailSender
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddTransient<IEmailSender<ApplicationUser>, EmailSender>();
    }
}