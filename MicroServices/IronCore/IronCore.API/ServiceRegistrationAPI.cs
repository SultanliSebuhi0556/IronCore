using IronCore.BL;
using IronCore.CORE.Entities;
using IronCore.CORE.Options;
using IronCore.DAL;
using IronCore.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;

namespace IronCore.API;

public static class ServiceRegistrationAPI
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDalServices(configuration);
        services.AddBlServices(configuration);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistrationAPI).Assembly));
        services.AddMapperProfiles();
        services.AddRabbitMQ();
        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
        })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
    private static IServiceCollection AddRabbitMQ(this IServiceCollection services)
    {
        services.AddSingleton<IConnection>(sp =>
        {
            var opt = sp.GetRequiredService<IOptions<RabbitMQOptions>>().Value;
            var factory = new ConnectionFactory { Uri = new Uri(opt.Uri) };
            return factory.CreateConnectionAsync().GetAwaiter().GetResult();
        });

        services.AddSingleton<IChannel>(sp =>
        {
            var connection = sp.GetRequiredService<IConnection>();
            return connection.CreateChannelAsync().GetAwaiter().GetResult();
        });
        return services;
    }
    private static IServiceCollection AddMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistrationAPI).Assembly);
        return services;
    }
    public static IServiceCollection ConfigureSwaggerAuthentication(this IServiceCollection services)
    {

        services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, Array.Empty<string>()
                }
            });
        });
        return services;
    }
}