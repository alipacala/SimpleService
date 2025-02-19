using System.Text.Json;
using Mapster;
using SimpleService.Application.Commands.Persona;
using SimpleService.Application.Common.Interfaces.Services;
using SimpleService.Domain.Entities;

public class CrearPersonaCommandHandler
{
    public static async Task<Persona> Handle(CrearPersonaCommand command, IPersonaService personaService)
    {
        var persona = command.Adapt<Persona>();
        return await personaService.AddAsync(persona);
    }
}