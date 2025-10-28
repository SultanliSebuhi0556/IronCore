using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SearchService.BL.Services.Implements;
using SearchService.BL.Services.Instances;
using SearchService.CORE.Options;

namespace SearchService.BL;
public static class ServiceRegistrationBL
{
    public static IServiceCollection AddBlServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScopes();
        services.AddCustomOptions(configuration);
        services.AddMapperProfiles();
        services.AddMassTransitConfiguration();
        return services;
    }

    private static IServiceCollection AddScopes(this IServiceCollection services)
    {
        services.AddScoped<IMessageElasticService, MessageElasticService>();
        return services;
    }
    private static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ElasticOptions>(configuration.GetSection(nameof(ElasticOptions)));
        services.Configure<RabbitMQOptions>(configuration.GetSection(nameof(RabbitMQOptions)));
        return services;
    }
    private static IServiceCollection AddMassTransitConfiguration(this IServiceCollection services)
    {
        services.AddMassTransit(opt =>
        {
            opt.SetDefaultEndpointNameFormatter();
            opt.AddConsumers(typeof(ServiceRegistrationBL).Assembly);
            opt.UsingRabbitMq((context, cfg) =>
            {
                var options = context.GetRequiredService<IOptions<RabbitMQOptions>>().Value;
                cfg.Host(options.Uri);
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
    private static IServiceCollection AddMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistrationBL).Assembly);
        return services;
    }
}
