using Microsoft.Extensions.Options;
using SearchService.CORE.Options;
using SearchService.CORE.RepositoryInstances;

namespace SearchService.API.Extensions;
public static class ApplicationExtensions
{
    public static async Task AddSeedData(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var elasticOption = scope.ServiceProvider.GetRequiredService<IOptions<ElasticOptions>>();
            var elasticRepo = scope.ServiceProvider.GetRequiredService<IMessageRepository>();
            await elasticRepo.CreateIndexIfNotExistAsync(elasticOption.Value.DefaultIndex, CancellationToken.None);

            //var rabbitOption = scope.ServiceProvider.GetRequiredService<IOptions<RabbitMQOptions>>();
            //var channel = scope.ServiceProvider.GetRequiredService<IChannel>();
            //foreach (var queue in rabbitOption.Value.Queues)
            //{
            //    await channel.QueueDeclareAsync(
            //        queue: queue.Name,
            //        durable: queue.Durable,
            //        exclusive: queue.Exclusive,
            //        autoDelete: queue.AutoDelete,
            //        arguments: null);
            //}
        }
    }
}