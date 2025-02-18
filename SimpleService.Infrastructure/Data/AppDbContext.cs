using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SimpleDomain.Application.Common.Interfaces;
using SimpleService.Domain.Entities;

namespace SimpleService.Infrastructure.Data;
public class AppDbContext : DbContext, IAppDbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  public DbSet<Persona> Personas => Set<Persona>();
  public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
  public DbSet<AuditLogDetail> AuditLogDetails => Set<AuditLogDetail>();

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    ApplyTableNames(builder);
    base.OnModelCreating(builder);
  }

  private void ApplyTableNames(ModelBuilder builder)
  {
    var configurationTypes = Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

    foreach (var configType in configurationTypes)
    {
      var configInstance = Activator.CreateInstance(configType);
      var entityType = configType.GetInterfaces()
          .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
          .GetGenericArguments()[0];

      var tableNameProperty = configType.GetProperty("TableName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      if (tableNameProperty != null)
      {
        var tableName = tableNameProperty.GetValue(configInstance)?.ToString();
        if (!string.IsNullOrEmpty(tableName))
        {
          builder.Entity(entityType).ToTable(tableName);
        }
      }
    }
  }
}