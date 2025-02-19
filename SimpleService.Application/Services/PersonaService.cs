using SimpleDomain.Application.Common.Interfaces;
using SimpleService.Application.Common.Interfaces.Services;
using SimpleService.Domain.Entities;

namespace SimpleService.Application.Services;
public class PersonaService : Service<Persona>, IPersonaService
{
  public PersonaService(IAppDbContext dbContext) : base(dbContext) { }
}