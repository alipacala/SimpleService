using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SimpleService.Application.Commands.Persona;
using SimpleService.Application.Common.Interfaces.Services;
using SimpleService.Application.Dtos.Persona;
using SimpleService.Application.Queries.Persona;
using SimpleService.Domain.Entities;
using Wolverine;

namespace SimpleService.Api.Endpoints;
public class PersonaEndpoints : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/personas");

        group.MapGet("/", async ([FromServices] IMessageBus bus) =>
        {
            return Results.Ok(await bus.InvokeAsync<IEnumerable<PersonaDto>>(new ListarPersonasQuery()));
        });

        group.MapGet("/{id}", async (Guid id, [FromServices] IMessageBus bus) =>
        {
            var persona = await bus.InvokeAsync<PersonaDto>(new ObtenerPersonaQuery(id));
            return persona != null ? Results.Ok(persona) : Results.NotFound();
        });

        group.MapPost("/", async ([FromBody] CrearPersonaCommand command, [FromServices] IMessageBus bus) =>
        {
            var created = await bus.InvokeAsync<Persona>(command);
            return Results.Created($"/api/personas/{created.Id}", created);
        });

        group.MapPatch("/{id}", async (Guid id, [FromBody] ActualizarPersonaCommand body, [FromServices] IMessageBus bus) =>
        {
            var command = body with { Id = id };
            await bus.InvokeAsync(command);
            return Results.NoContent();
        });
    }
}