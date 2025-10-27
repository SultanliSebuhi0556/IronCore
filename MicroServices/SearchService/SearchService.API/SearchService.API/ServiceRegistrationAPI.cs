using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SearchService.BL;
using SearchService.CORE.Options;
using SearchService.DAL;

namespace SearchService.API;
public static class ServiceRegistrationAPI
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBlServices(configuration);
        services.AddDalServices(configuration);
        services.AddCustomOptions(configuration);
        services.AddRabbitMQ();
        return services;
    }
    private static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ElasticOptions>(configuration.GetSection(nameof(ElasticOptions)));
        services.Configure<RabbitMQOptions>(configuration.GetSection(nameof(RabbitMQOptions)));
        return services;
    }
    private static IServiceCollection AddRabbitMQ(this IServiceCollection services)
    {
        services.AddSingleton<IConnection>(sp =>
        {
            var opt = sp.GetRequiredService<IOptions<RabbitMQOptions>>().Value;
            var factory = new ConnectionFactory { Uri = new Uri(opt.Uri) };
            return factory.CreateConnectionAsync().GetAwaiter().GetResult();
        });

        services.AddSingleton<IChannel>(sp =>
        {
            var connection = sp.GetRequiredService<IConnection>();
            return connection.CreateChannelAsync().GetAwaiter().GetResult();
        });
        return services;
    }
}