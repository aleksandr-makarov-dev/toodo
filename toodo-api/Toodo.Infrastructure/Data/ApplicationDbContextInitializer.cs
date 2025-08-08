using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Toodo.Domain.Constants;
using Toodo.Domain.Entities;
using Toodo.Infrastructure.Identity;

namespace Toodo.Infrastructure.Data;

public static class ApplicationDbContextInitializerExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initializer.InitializeAsync();
        await initializer.SeedAsync();
    }
}

public class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager
)
{
    public async Task InitializeAsync()
    {
        try
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            // Default roles
            var roles = new List<IdentityRole>
            {
                new(Roles.User),
                new(Roles.Admin)
            };
            
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            // Admin user
            var admin = new ApplicationUser
            {
                UserName = "adminuser@example.com",
                Email = "adminuser@example.com",
                EmailConfirmed = true,
            };

            await userManager.CreateAsync(admin, "Admin123!");
            await userManager.AddToRoleAsync(admin, Roles.Admin);

            // Ordinary user

            var user = new ApplicationUser
            {
                UserName = "user@example.com",
                Email = "user@example.com",
                EmailConfirmed = true,
            };

            await userManager.CreateAsync(user, "User123!");
            await userManager.AddToRoleAsync(user, Roles.User);

            // Items

            var issues = new List<Issue>
            {
                new()
                {
                    Title = "Bug in user authentication",
                    Description = "Users are unable to log in after password reset."
                },
                new()
                {
                    Title = "Feature request: Dark mode",
                    Description = "Add a dark mode option to improve user experience in low-light conditions."
                },
                new()
                {
                    Title = "Performance degradation on dashboard",
                    Description = "The dashboard takes over 10 seconds to load with a large amount of data."
                },
                new()
                {
                    Title = "UI/UX inconsistency on mobile",
                    Description = "Buttons and fonts are not consistent on smaller screens."
                },
                new()
                {
                    Title = "Database connection timeout",
                    Description =
                        "The application occasionally fails to connect to the database, causing a server error."
                },
                new()
                {
                    Title = "Update dependencies to latest versions",
                    Description =
                        "Update all NuGet packages to their latest stable versions for security and performance improvements."
                }
            };

            context.Issues.AddRange(issues);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
}