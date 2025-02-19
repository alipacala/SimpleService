namespace SimpleService.Domain.Entities;
public class AuditLog
{
  public Guid Id { get; private set; } = Guid.NewGuid();
  public string TableName { get; set; } = string.Empty;
  public Guid EntityId { get; set; } = Guid.Empty;

  public DateTime EntityModifiedAt { get; private set; } = DateTime.UtcNow;
  public Guid EntityModifiedBy { get; set; } = Guid.Empty;

  public IEnumerable<AuditLogDetail> Details { get; set; } = new List<AuditLogDetail>();
}