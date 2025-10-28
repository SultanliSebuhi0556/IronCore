using SearchService.BL;
using SearchService.DAL;

namespace SearchService.API;
public static class ServiceRegistrationAPI
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBlServices(configuration);
        services.AddDalServices(configuration);
        return services;
    }
}