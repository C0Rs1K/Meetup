using AutoMapper;
using Meetup.Application.Services;
using Meetup.Application.Services.Intarfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace Meetup.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                .AddAutoMapper()
                .AddServices();
        }

        private static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.IsClass
                                && !type.IsAbstract
                                && type.IsSubclassOf(typeof(Profile)))
                .ToArray();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(types);
                cfg.DisableConstructorMapping();
            });

            services.AddSingleton(mapperConfig.CreateMapper());

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes();

            var interfaceTypes = types
                .Where(type => type.IsInterface
                                && type.Namespace == typeof(IMeetupService).Namespace)
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
}
