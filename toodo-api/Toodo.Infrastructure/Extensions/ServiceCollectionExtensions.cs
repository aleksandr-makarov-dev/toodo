using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Toodo.Infrastructure.Data;
using Toodo.Infrastructure.Data.Interceptors;
using Toodo.Infrastructure.Identity;

namespace Toodo.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register TimeProvider
        services.AddSingleton(TimeProvider.System);

        // Register AuditableEntityInterceptor to update CreatedAt and UpdatedAt properties 
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        // Register ApplicationDbContext
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            options.AddInterceptors(serviceProvider.GetRequiredService<ISaveChangesInterceptor>());
        });

        // Register Identity
        services.AddIdentityApiEndpoints<ApplicationUser>((options) =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<ApplicationDbContextInitializer>();
    }
}