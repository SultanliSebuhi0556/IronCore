using SearchService.CORE.Entities;
using SearchService.CORE.RepositoryInstances;

namespace SearchService.API.Extensions;
public static class ApplicationExtensions
{
    public static async Task AddSeedData(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var elasticRepo = scope.ServiceProvider.GetRequiredService<IMessageRepository>();
            await elasticRepo.CreateIndexIfNotExistAsync(Message.IndexName, CancellationToken.None);
        }
    }
}