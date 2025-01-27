using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.ValueObjects;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;

namespace SMAIAXBackend.Infrastructure.EntityConfigurations;

public class DeviceMappingConfiguration : IEntityTypeConfiguration<DeviceMapping>
{
    public void Configure(EntityTypeBuilder<DeviceMapping> builder)
    {
        builder.ToTable("DeviceMapping", "domain");
        
        builder.HasKey(d => d.ConnectorSerialNumber);
        
        builder.Property(d => d.ConnectorSerialNumber)
            .HasConversion(
                v => v.SerialNumber,
                v => new ConnectorSerialNumber(v))
            .IsRequired();

        builder.Property(d => d.PublicKey)
            .IsRequired();

        builder.Property(d => d.AssignedUser)
            .HasConversion(
                v => v != null ? (Guid?)v.Id : null,
                v => v.HasValue ? new UserId(v.Value) : null);
    }
}