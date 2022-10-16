using Meetup.Infrastracture.DataBase;
using Meetup.Infrastracture.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Meetup.Infrastracture;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services
            .AddSqlServer(connectionString)
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddSqlServer(this IServiceCollection services, string connectionString)
    {
        services
            .AddDbContext<MeetupDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString, opt =>
                {
                    opt.MigrationsAssembly(typeof(MeetupDbContext).Assembly.FullName);
                });
            });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var types = Assembly
                .GetExecutingAssembly()
                .GetTypes();

        var interfaceTypes = types
            .Where(type => type.IsInterface
                            && type.Namespace == typeof(IMeetupRepository).Namespace)
            .ToArray();

        foreach (var interfaceType in interfaceTypes)
        {
            var implementation = types
                .FirstOrDefault(type => type.GetInterface(interfaceType.Name) == interfaceType);

            services
                .AddScoped(interfaceType, implementation);
        }

        return services;
    }
}
