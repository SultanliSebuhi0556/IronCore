using FluentValidation;
using FluentValidation.AspNetCore;
using IronCore.BL.ExternalServices.Implements;
using IronCore.BL.ExternalServices.Instances;
using IronCore.BL.Services.Implements;
using IronCore.BL.Services.Instances;
using IronCore.CORE.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IronCore.BL;
public static class ServiceRegistrationsBL
{
    public static IServiceCollection AddBlServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScopes();
        services.AddCustomOptions(configuration);
        services.AddMassTransitConfiguration();
        services.AddFluentValidation();
        return services;
    }
    private static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWTOptions>(configuration.GetSection($"ExternalServices:{nameof(JWTOptions)}"));
        services.Configure<RabbitMQOptions>(configuration.GetSection($"ExternalServices:{nameof(RabbitMQOptions)}"));
        return services;
    }
    private static IServiceCollection AddMassTransitConfiguration(this IServiceCollection services)
    {
        services.AddMassTransit(opt =>
        {
            opt.SetDefaultEndpointNameFormatter();
            opt.AddConsumers(typeof(ServiceRegistrationsBL).Assembly);
            opt.UsingRabbitMq((context, config) =>
            {
                var options = context.GetRequiredService<IOptions<RabbitMQOptions>>().Value;
                config.Host(options.Uri);
            });
        });
        return services;
    }
    private static IServiceCollection AddScopes(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IChannelServices, ChannelServices>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IFileHelper, FileHelper>();
        services.AddScoped<IStorageService, StorageService>();
        return services;
    }
    private static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(typeof(ServiceRegistrationsBL));
    }
}