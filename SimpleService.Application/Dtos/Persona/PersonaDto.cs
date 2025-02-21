using Reinforced.Typings.Attributes;

namespace SimpleService.Application.Dtos.Persona;
[TsInterface]
public record PersonaDto (Guid Id, string Nombre);