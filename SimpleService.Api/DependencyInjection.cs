using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SimpleService.Application.Common.Interfaces.Services;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
  public static IServiceCollection AddApiServices(this IServiceCollection services, Assembly assembly)
{
    var serviceTypes = assembly.GetTypes()
        .Where(t => t.IsClass && !t.IsAbstract) // Solo clases concretas
        .SelectMany(t => t.GetInterfaces(), (impl, iface) => new { impl, iface })
        .Where(t => t.iface.IsGenericType && t.iface.GetGenericTypeDefinition() == typeof(IService<>))
        .ToList();

    foreach (var serviceType in serviceTypes)
    {
        Console.WriteLine($"Registrando: {serviceType.impl.Name} como {serviceType.iface.Name}");

        services.AddScoped(serviceType.iface, serviceType.impl); // Registro directo sin reflexión extra
    }

    return services;
}

  public static IServiceCollection AddEndpoints(
    this IServiceCollection services,
    Assembly assembly)
  {
    ServiceDescriptor[] serviceDescriptors = assembly
        .DefinedTypes
        .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                       type.IsAssignableTo(typeof(IEndpoint)))
        .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
        .ToArray();

    services.TryAddEnumerable(serviceDescriptors);

    return services;
  }

  public static IApplicationBuilder MapEndpoints(
    this WebApplication app,
    RouteGroupBuilder? routeGroupBuilder = null)
  {
    IEnumerable<IEndpoint> endpoints = app.Services
        .GetRequiredService<IEnumerable<IEndpoint>>();

    IEndpointRouteBuilder builder =
        routeGroupBuilder is null ? app : routeGroupBuilder;

    foreach (IEndpoint endpoint in endpoints)
    {
      endpoint.MapEndpoints(builder);
    }

    return app;
  }
}