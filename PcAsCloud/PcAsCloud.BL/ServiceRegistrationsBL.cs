using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PcAsCloud.BL.ExternalServices.Implements;
using PcAsCloud.BL.ExternalServices.Instances;
using PcAsCloud.BL.Options;
using PcAsCloud.BL.Services.Implements;
using PcAsCloud.BL.Services.Instances;
using PcAsCloud.BL.Validators.Channel;
using PcAsCloud.BL.Validators.Message;

namespace PcAsCloud.BL;
public static class ServiceRegistrationsBL
{
    public static IServiceCollection AddBlServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddServices();
        services.AddCustomOptions(configuration);
        services.AddFluentValidation();
        services.AddMapperProfiles();
        return services;
    }
    private static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWTOptions>(configuration.GetSection("ExternalServices:JwtOptions"));
        return services;
    }
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IChannelServices, ChannelServices>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        return services;
    }
    private static IServiceCollection AddMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistrationsBL).Assembly);
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