namespace SimpleService.Domain.Entities;
public class AuditLogDetail
{
  public Guid Id { get; private set; } = Guid.NewGuid();
  public string FieldName { get; set; } = string.Empty;
  public string Value { get; set; } = string.Empty;

  public AuditLog AuditLog { get; set; } = null!;
}