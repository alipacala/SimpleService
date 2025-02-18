namespace SimpleService.Domain.Entities;
public class EntityBase {
  public Guid Id { get; set; } = Guid.NewGuid();
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? DeletedAt { get; set; }
  public bool IsDeleted { get; set; } = false;

  public void Delete()
  {
    if (IsDeleted) throw new Exception("La entidad ya ha sido eliminada");

    IsDeleted = true;
    DeletedAt = DateTime.UtcNow;
  }
}