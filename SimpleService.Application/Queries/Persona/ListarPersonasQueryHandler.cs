using Mapster;
using SimpleService.Application.Common.Interfaces.Services;
using SimpleService.Application.Dtos.Persona;
using SimpleService.Application.Queries.Persona;

public class ListarPersonasQueryHandler
{
    public static async Task<IEnumerable<PersonaDto>> Handle(ListarPersonasQuery query, IPersonaService personaService)
    {
        return (await personaService.GetAllAsync()).Adapt<IEnumerable<PersonaDto>>();
    }
}