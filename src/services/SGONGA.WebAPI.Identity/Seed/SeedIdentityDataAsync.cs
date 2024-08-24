using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SGONGA.WebAPI.Identity.Context;
using System.Security.Claims;

namespace SGONGA.WebAPI.Identity.Seed;
public static class SeedIdentityDataAsync
{
    public static async Task SeedIdentityAsync(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userId = Guid.Parse("2f805cb2-1c01-4d88-92ec-a989bad5b0af");

            var adminEmail = "tenant@email.com";
            var adminPassword = "Admin@123";

            if (!await roleManager.RoleExistsAsync("SuperAdmin"))
            {
                var role = new IdentityRole("SuperAdmin");
                await roleManager.CreateAsync(role);
            }

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    Id = userId.ToString(),
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "SuperAdmin");
                }
            }

            var hasSuperAdminClaim = (await userManager.GetClaimsAsync(adminUser)).Any(c => c.Type == "Permissions" && c.Value == "SuperAdmin");
            if (!hasSuperAdminClaim)
            {
                await userManager.AddClaimAsync(adminUser, new Claim("Permissions", "SuperAdmin"));
            }
        }
    }
}