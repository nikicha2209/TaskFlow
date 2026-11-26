using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TaskFlow.Web.Common.Constants;

namespace TaskFlow.Web.Infrastructure.Identity;

public static class IdentityRoleSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = [RoleConstants.User, RoleConstants.Administrator];

        foreach (var roleName in roles)
        {
            if (await roleManager.RoleExistsAsync(roleName))
            {
                continue;
            }

            var result = await roleManager.CreateAsync(new IdentityRole(roleName));

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"Could not create the '{roleName}' role: {errors}");
            }
        }
    }
}
