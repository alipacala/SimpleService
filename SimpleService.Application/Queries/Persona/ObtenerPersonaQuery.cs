using Reinforced.Typings.Attributes;

namespace SimpleService.Application.Queries.Persona;
[TsInterface]
public class ObtenerPersonaQuery (Guid id) {
  public Guid Id { get; set; } = id;
}