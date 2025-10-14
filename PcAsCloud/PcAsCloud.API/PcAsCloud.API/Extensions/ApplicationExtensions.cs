using Microsoft.AspNetCore.Identity;
using PcAsCloud.CORE.Entities;
using PcAsCloud.DAL.Context;

namespace PcAsCloud.API.Extensions;
public static class ApplicationExtensions
{
    public static async Task AddSeedData(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await AppDbContextSeed.SeedDatabaseAsync(context, userManager, roleManager);
        }
    }
}