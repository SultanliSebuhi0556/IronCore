using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchService.BL.MessageBroker.Implementations;
using SearchService.BL.MessageBroker.Interfaces;
using SearchService.BL.Services.Implements;
using SearchService.BL.Services.Instances;

namespace SearchService.BL;
public static class ServiceRegistrationBL
{
    public static IServiceCollection AddBlServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddScopes(services);
        AddMapperProfiles(services);
        return services;
    }

    private static IServiceCollection AddScopes(this IServiceCollection services)
    {
        services.AddScoped<IRabbitMqConsumer, RabbitMqConsumer>();
        services.AddScoped<IMessageElasticService, MessageElasticService>();
        return services;
    }
    private static IServiceCollection AddMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistrationBL).Assembly);
        return services;
    }
}
