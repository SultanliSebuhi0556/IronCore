using IronCore.CORE.Entities;
using IronCore.CORE.Options;
using IronCore.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace IronCore.API.Extensions;
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

            var rabbitOption = scope.ServiceProvider.GetRequiredService<IOptions<RabbitMQOptions>>();
            var channel = scope.ServiceProvider.GetRequiredService<IChannel>();
            foreach (var queue in rabbitOption.Value.Queues)
            {
                await channel.QueueDeclareAsync(
                    queue: queue.Name,
                    durable: queue.Durable,
                    exclusive: queue.Exclusive,
                    autoDelete: queue.AutoDelete,
                    arguments: null);
            }
        }
    }
}