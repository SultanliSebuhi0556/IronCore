using IronCore.CORE.Entities;
using IronCore.CORE.Enums;
using Microsoft.AspNetCore.Identity;

namespace IronCore.DAL.Context;

public static class AppDbContextSeed
{
    public static async Task SeedDatabaseAsync(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> _roleManager)
    {
        // Seed roles
        foreach (var roleName in Enum.GetNames(typeof(UserRoles)))
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        // Seed admin
        //var adminExists = await userManager.FindByEmailAsync("admin@admin.com");
        //if (adminExists == null)
        //{
        //    var userAdmin = new AppUser { Name = "Admin", Surname = "Admin", UserName = "Admin", Email = "admin@admin.com", EmailConfirmed = true };
        //    var result = await userManager.CreateAsync(userAdmin, "AdminAdmin123!");

        //    if (result.Succeeded)
        //    {
        //        await userManager.AddToRoleAsync(userAdmin, UserRole.Admin.ToString());
        //    }
        //    else
        //    {
        //        throw new Exception("Failed to create admin user: " + string.Join("; ", result.Errors.Select(e => e.Description)));
        //    }
        //}
    }
}