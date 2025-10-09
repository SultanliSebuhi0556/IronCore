using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PcAsCloud.BL.ExternalServices.Storage;
using PcAsCloud.BL.Services.Services.Implements;
using PcAsCloud.BL.Services.Services.Instances;
using PcAsCloud.BL.Validators.Channel;
using PcAsCloud.BL.Validators.Message;

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
        services.AddScoped<ISaveFileService, SaveFileService>();
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
        services.AddValidatorsFromAssemblyContaining<MessageCreateDTOValidator>();
        services.AddValidatorsFromAssemblyContaining<ChannelCreateDTOValidator>();
    }
}