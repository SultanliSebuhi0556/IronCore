using FluentValidation;
using FluentValidation.AspNetCore;
using IronCore.BL.ExternalServices.Implements;
using IronCore.BL.ExternalServices.Instances;
using IronCore.BL.Services.Implements;
using IronCore.BL.Services.Instances;
using IronCore.CORE.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IronCore.BL;
public static class ServiceRegistrationsBL
{
    public static IServiceCollection AddBlServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScopes();
        services.AddCustomOptions(configuration);
        services.AddFluentValidation();
        return services;
    }
    private static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWTOptions>(configuration.GetSection($"ExternalServices:{nameof(JWTOptions)}"));
        services.Configure<RabbitMQOptions>(configuration.GetSection($"ExternalServices:{nameof(RabbitMQOptions)}"));
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
        services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();
        return services;
    }
    private static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(typeof(ServiceRegistrationsBL));
    }
}