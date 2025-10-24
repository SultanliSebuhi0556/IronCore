using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchService.CORE.RepositoryInstances;
using SearchService.DAL.RepositoryImplements;

namespace SearchService.DAL;
public static class ServiceRegistrationDAL
{
    public static IServiceCollection AddDalServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddScopes(services);
        return services;
    }

    private static IServiceCollection AddScopes(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IMessageRepository, MessageRepository>();
        return services;
    }
}