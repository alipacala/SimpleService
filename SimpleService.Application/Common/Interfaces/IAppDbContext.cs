using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleService.Domain.Entities;

namespace SimpleDomain.Application.Common.Interfaces;
public interface IAppDbContext
{
  DbSet<Persona> Personas { get; }
  DbSet<AuditLog> AuditLogs { get; }
  DbSet<AuditLogDetail> AuditLogDetails { get; }
  DbSet<T> Set<T>() where T : class;
  EntityEntry<T> Entry<T>(T entity) where T : class;
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}