using SearchService.CORE.Options;
using SearchService.DAL;

namespace SearchService.API;
public static class ServiceRegistrationAPI
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDalServices(configuration);
        services.AddCustomOptions(configuration);
        return services;
    }
    private static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ElasticOptions>(configuration.GetSection(nameof(ElasticOptions)));
        services.Configure<RabbitMQOptions>(configuration.GetSection(nameof(RabbitMQOptions)));
        return services;
    }
}