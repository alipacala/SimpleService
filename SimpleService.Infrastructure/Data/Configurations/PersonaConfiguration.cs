using SimpleService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleService.Infrastructure.Data.Configurations;
public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
  public string TableName => "Personas";
  public void Configure(EntityTypeBuilder<Persona> builder)
  {
    builder.Property(t => t.Nombre)
        .HasMaxLength(100)
        .IsRequired();
  }
}