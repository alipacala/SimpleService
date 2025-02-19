using Mapster;
using SimpleService.Application.Commands.Persona;
using SimpleService.Application.Common.Interfaces.Services;

public class ActualizarPersonaCommandHandler
{
    public static async Task Handle(ActualizarPersonaCommand command, IPersonaService personaService)
    {
        var persona = await personaService.GetByIdAsync(command.Id);
        command.Adapt(persona);
        await personaService.UpdateAsync(persona);
    }
}