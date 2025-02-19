
using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SimpleDomain.Application.Common.Interfaces;
using SimpleService.Application.Common.Interfaces.Services;
using SimpleService.Domain.Entities;

namespace SimpleService.Application.Services;
public abstract class Service<T> : IService<T> where T : class
{
    private readonly IAppDbContext _dbContext;

    public Service(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id) ?? throw new Exception("La entidad no ha sido encontrada");
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        var details = GetAuditLogDetails(entity);

        if (details.Any())
        {
            _dbContext.Set<AuditLog>().Add(new()
            {
                TableName = GetTableName() ?? string.Empty,
                EntityId = Guid.Parse(entity.GetType().GetProperty("Id")?.GetValue(entity)?.ToString() ?? string.Empty),
                EntityModifiedBy = Guid.NewGuid(), // TODO: Obtener el Id del usuario actual
                Details = details
            });
        }

        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    private string? GetTableName()
    {
        var configType = Assembly.GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(t => t.IsClass &&
                                 !t.IsAbstract &&
                                 t.GetInterfaces().Any(i => i.IsGenericType &&
                                                            i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<T>)));

        if (configType is null)
            return null;

        // 🔹 Instancia la configuración
        var configInstance = Activator.CreateInstance(configType);

        return configType.GetProperty("TableName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                         ?.GetValue(configInstance)
                         ?.ToString();
    }

    private IEnumerable<AuditLogDetail> GetAuditLogDetails(T entity)
    {
        var entry = _dbContext.Entry(entity);
        var details = new List<AuditLogDetail>();

        var properties = entity.GetType()
            .GetProperties()
            .Where(p => p.DeclaringType == entity.GetType());

        foreach (var property in properties)
        {
            var originalValue = entry.Property(property.Name).OriginalValue;
            var modifiedValue = entry.Property(property.Name).CurrentValue;

            Console.WriteLine($"Property: {property.Name}, Original Value: {originalValue}, Modified Value: {modifiedValue}");

            if (!Equals(originalValue, modifiedValue))
            {
                details.Add(new()
                {
                    FieldName = property.Name,
                    Value = originalValue?.ToString() ?? string.Empty,
                });
            }
        }

        return details;
    }
}