using PcAsCloud.BL;
using PcAsCloud.DAL;

namespace PcAsCloud.API;

public static class ServiceRegistrationAPI
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDalServices(configuration);
        services.AddBlServices();
        return services;
    }
}