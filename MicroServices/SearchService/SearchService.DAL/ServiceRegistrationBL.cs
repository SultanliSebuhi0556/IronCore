using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchService.CORE.RepositoryInstances;
using SearchService.DAL.Options;
using SearchService.DAL.RepositoryImplements;

namespace SearchService.DAL;
public static class ServiceRegistrationBL
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddElasticsearch(services, configuration);

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IMessageRepository, MessageRepository>();
        return services;
    }
    private static IServiceCollection AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ElasticOptions>(configuration.GetSection(nameof(ElasticOptions)));
        return services;
    }
}