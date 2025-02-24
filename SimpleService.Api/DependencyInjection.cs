using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SimpleService.Api.Endpoints;
using SimpleService.Application.Common.Interfaces.Services;
using SimpleService.Application.Services;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddScoped<IPersonaService, PersonaService>();
    return services;
  }

  public static IServiceCollection AddEndpoints(this IServiceCollection services)
  {
    services.Scan(scan => scan
        .FromAssemblies(Assembly.GetExecutingAssembly())
        .AddClasses(classes => classes.AssignableTo<IEndpoint>())
        .AsImplementedInterfaces()
        .WithTransientLifetime()
    );
    return services;
  }

  public static WebApplication MapEndpoints(this WebApplication app)
  {
    var endpoints = app.Services.GetServices<IEndpoint>();
    foreach (var endpoint in endpoints)
    {
      endpoint.MapEndpoint(app);
    }
    return app;
  }
}