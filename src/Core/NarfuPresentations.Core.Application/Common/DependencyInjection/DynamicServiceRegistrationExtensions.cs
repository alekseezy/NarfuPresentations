using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

namespace NarfuPresentations.Core.Application.Common.DependencyInjection;

public static class DynamicServiceRegistrationExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddServices(typeof(ITransientService), ServiceLifetime.Transient)
            .AddServices(typeof(IScopedService), ServiceLifetime.Scoped);

    public static IServiceCollection AddServices(this IServiceCollection services,
        Type interfaceType,
        ServiceLifetime lifetime)
    {
        var serviceDefinitions = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => interfaceType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
            .Select(type =>
                new ServiceDefinition(type.GetInterfaces().FirstOrDefault()!, type, lifetime))
            .Where(serviceDefinition => serviceDefinition.Service is not null &&
                                        interfaceType.IsAssignableFrom(serviceDefinition.Service));

        foreach (var serviceDefinition in serviceDefinitions)
            services.AddService(serviceDefinition);

        return services;
    }

    [UsedImplicitly]
    private static IServiceCollection AddService(this IServiceCollection services,
        ServiceDefinition serviceDefinition) =>
        serviceDefinition.Lifetime switch
        {
            ServiceLifetime.Transient => services.AddTransient(serviceDefinition.Service!,
                serviceDefinition.Implementation!),
            ServiceLifetime.Scoped => services.AddScoped(serviceDefinition.Service!,
                serviceDefinition.Implementation!),
            ServiceLifetime.Singleton => services.AddSingleton(serviceDefinition.Service!,
                serviceDefinition.Implementation!),
            _ => throw new ArgumentException("Invalid Service Lifetime",
                nameof(serviceDefinition))
        };

    private record ServiceDefinition(Type? Service, Type? Implementation, ServiceLifetime Lifetime);
}
