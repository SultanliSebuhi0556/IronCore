using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PcAsCloud.CORE.RepositoryInstances;
using PcAsCloud.DAL.Context;
using PcAsCloud.DAL.RepositoryImplements;

namespace PcAsCloud.DAL;
public static class ServiceRegistrationsDAL
{
    public static IServiceCollection AddDalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
        services.AddDbContext(configuration);
        return services;
    }
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IChannelRepository, ChannelRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));
        });
        return services;
    }
}