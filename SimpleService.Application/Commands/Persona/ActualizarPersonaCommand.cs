using Reinforced.Typings.Attributes;

namespace SimpleService.Application.Commands.Persona;
[TsInterface]
public record ActualizarPersonaCommand(Guid Id, string Nombre);