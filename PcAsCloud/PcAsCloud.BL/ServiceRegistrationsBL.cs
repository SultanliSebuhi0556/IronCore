using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PcAsCloud.BL.Helpers.Implements;
using PcAsCloud.BL.Helpers.Instances;
using PcAsCloud.BL.Services.Services.Implements;
using PcAsCloud.BL.Services.Services.Instances;

namespace PcAsCloud.BL;
public static class ServiceRegistrationsBL
{
    public static IServiceCollection AddBlServices(this IServiceCollection services)
    {
        services.AddServices();
        services.AddFluentValidation();
        services.AddMapperProfiles();
        return services;
    }
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IChannelServices, ChannelServices>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IFileHelper, FileHelper>();
        services.AddScoped<IStorageService, StorageService>();
        return services;
    }
    private static IServiceCollection AddMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistrationsBL));
        return services;
    }
    private static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(typeof(ServiceRegistrationsBL));
    }
}