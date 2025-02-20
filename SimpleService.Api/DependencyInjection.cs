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

  public static IServiceCollection AddEndpoints(
    this IServiceCollection services)
  {
    services.AddTransient<IEndpoint, PersonaEndpoints>();

    return services;
  }

  public static IApplicationBuilder MapEndpoints(
    this WebApplication app)
  {
    var personaEndpoints = app.Services.GetRequiredService<PersonaEndpoints>();
    personaEndpoints.MapEndpoints(app);

    return app;
  }
}