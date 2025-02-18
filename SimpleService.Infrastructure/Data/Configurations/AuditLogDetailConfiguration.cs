using SimpleService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleService.Infrastructure.Data.Configurations;
public class AuditLogDetailConfiguration : IEntityTypeConfiguration<AuditLogDetail>
{
    public string TableName => "AuditLogDetails";
    public void Configure(EntityTypeBuilder<AuditLogDetail> builder) { }
}