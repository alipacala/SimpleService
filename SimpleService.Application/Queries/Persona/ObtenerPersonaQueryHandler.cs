using Mapster;
using SimpleService.Application.Common.Interfaces.Services;
using SimpleService.Application.Dtos.Persona;
using SimpleService.Application.Queries.Persona;

public class ObtenerPersonaQueryHandler
{
    public static async Task<PersonaDto> Handle(ObtenerPersonaQuery query, IPersonaService personaService)
    {
        return (await personaService.GetByIdAsync(query.Id)).Adapt<PersonaDto>();
    }
}