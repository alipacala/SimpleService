using SimpleService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleService.Infrastructure.Data.Configurations;
public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public string TableName => "AuditLogs";
    public void Configure(EntityTypeBuilder<AuditLog> builder) { }
}