using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using SimpleDomain.Application.Common.Interfaces;
using SimpleService.Infrastructure.Data;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? configuration.GetConnectionString("DefaultConnection");

    services.AddDbContext<AppDbContext>((sp, options) =>
    {
      options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
      options.UseNpgsql(connectionString);
    });

    services.AddScoped<IAppDbContext, AppDbContext>();

    return services;
  }
}