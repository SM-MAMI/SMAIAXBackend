using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SMAIAXBackend.Domain.Model.Entities.Measurements;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;

namespace SMAIAXBackend.Infrastructure.EntityConfigurations;

public class MeasurementPerMinuteConfiguration : IEntityTypeConfiguration<MeasurementPerMinute>
{
    public void Configure(EntityTypeBuilder<MeasurementPerMinute> builder)
    {
        builder.ToView("MeasurementPerMinute", "domain").HasNoKey();

        builder.Property(m => m.PositiveActivePower).HasColumnName("positiveActivePower");
        builder.Property(m => m.PositiveActiveEnergyTotal).HasColumnName("positiveActiveEnergyTotal");
        builder.Property(m => m.NegativeActivePower).HasColumnName("negativeActivePower");
        builder.Property(m => m.NegativeActiveEnergyTotal).HasColumnName("negativeActiveEnergyTotal");
        builder.Property(m => m.ReactiveEnergyQuadrant1Total).HasColumnName("reactiveEnergyQuadrant1Total");
        builder.Property(m => m.ReactiveEnergyQuadrant3Total).HasColumnName("reactiveEnergyQuadrant3Total");
        builder.Property(m => m.TotalPower).HasColumnName("totalPower");
        builder.Property(m => m.CurrentPhase1).HasColumnName("currentPhase1");
        builder.Property(m => m.VoltagePhase1).HasColumnName("voltagePhase1");
        builder.Property(m => m.CurrentPhase2).HasColumnName("currentPhase2");
        builder.Property(m => m.VoltagePhase2).HasColumnName("voltagePhase2");
        builder.Property(m => m.CurrentPhase3).HasColumnName("currentPhase3");
        builder.Property(m => m.VoltagePhase3).HasColumnName("voltagePhase3");
        builder.Property(m => m.Uptime).HasColumnName("uptime");
        builder.Property(m => m.Timestamp).HasColumnName("timestamp");

        builder.Property(m => m.SmartMeterId)
            .HasConversion(
                v => v.Id,
                v => new SmartMeterId(v));
    }
}

public class MeasurementPerQuarterHourConfiguration : IEntityTypeConfiguration<MeasurementPerQuarterHour>
{
    public void Configure(EntityTypeBuilder<MeasurementPerQuarterHour> builder)
    {
        builder.ToView("MeasurementPerQuarterHour", "domain").HasNoKey();

        builder.Property(m => m.PositiveActivePower).HasColumnName("positiveActivePower");
        builder.Property(m => m.PositiveActiveEnergyTotal).HasColumnName("positiveActiveEnergyTotal");
        builder.Property(m => m.NegativeActivePower).HasColumnName("negativeActivePower");
        builder.Property(m => m.NegativeActiveEnergyTotal).HasColumnName("negativeActiveEnergyTotal");
        builder.Property(m => m.ReactiveEnergyQuadrant1Total).HasColumnName("reactiveEnergyQuadrant1Total");
        builder.Property(m => m.ReactiveEnergyQuadrant3Total).HasColumnName("reactiveEnergyQuadrant3Total");
        builder.Property(m => m.TotalPower).HasColumnName("totalPower");
        builder.Property(m => m.CurrentPhase1).HasColumnName("currentPhase1");
        builder.Property(m => m.VoltagePhase1).HasColumnName("voltagePhase1");
        builder.Property(m => m.CurrentPhase2).HasColumnName("currentPhase2");
        builder.Property(m => m.VoltagePhase2).HasColumnName("voltagePhase2");
        builder.Property(m => m.CurrentPhase3).HasColumnName("currentPhase3");
        builder.Property(m => m.VoltagePhase3).HasColumnName("voltagePhase3");
        builder.Property(m => m.Uptime).HasColumnName("uptime");
        builder.Property(m => m.Timestamp).HasColumnName("timestamp");

        builder.Property(m => m.SmartMeterId)
            .HasConversion(
                v => v.Id,
                v => new SmartMeterId(v));
    }
}

public class MeasurementPerHourConfiguration : IEntityTypeConfiguration<MeasurementPerHour>
{
    public void Configure(EntityTypeBuilder<MeasurementPerHour> builder)
    {
        builder.ToView("MeasurementPerHour", "domain").HasNoKey();

        builder.Property(m => m.PositiveActivePower).HasColumnName("positiveActivePower");
        builder.Property(m => m.PositiveActiveEnergyTotal).HasColumnName("positiveActiveEnergyTotal");
        builder.Property(m => m.NegativeActivePower).HasColumnName("negativeActivePower");
        builder.Property(m => m.NegativeActiveEnergyTotal).HasColumnName("negativeActiveEnergyTotal");
        builder.Property(m => m.ReactiveEnergyQuadrant1Total).HasColumnName("reactiveEnergyQuadrant1Total");
        builder.Property(m => m.ReactiveEnergyQuadrant3Total).HasColumnName("reactiveEnergyQuadrant3Total");
        builder.Property(m => m.TotalPower).HasColumnName("totalPower");
        builder.Property(m => m.CurrentPhase1).HasColumnName("currentPhase1");
        builder.Property(m => m.VoltagePhase1).HasColumnName("voltagePhase1");
        builder.Property(m => m.CurrentPhase2).HasColumnName("currentPhase2");
        builder.Property(m => m.VoltagePhase2).HasColumnName("voltagePhase2");
        builder.Property(m => m.CurrentPhase3).HasColumnName("currentPhase3");
        builder.Property(m => m.VoltagePhase3).HasColumnName("voltagePhase3");
        builder.Property(m => m.Uptime).HasColumnName("uptime");
        builder.Property(m => m.Timestamp).HasColumnName("timestamp");

        builder.Property(m => m.SmartMeterId)
            .HasConversion(
                v => v.Id,
                v => new SmartMeterId(v));
    }
}

public class MeasurementPerDayConfiguration : IEntityTypeConfiguration<MeasurementPerDay>
{
    public void Configure(EntityTypeBuilder<MeasurementPerDay> builder)
    {
        builder.ToView("MeasurementPerDay", "domain").HasNoKey();

        builder.Property(m => m.PositiveActivePower).HasColumnName("positiveActivePower");
        builder.Property(m => m.PositiveActiveEnergyTotal).HasColumnName("positiveActiveEnergyTotal");
        builder.Property(m => m.NegativeActivePower).HasColumnName("negativeActivePower");
        builder.Property(m => m.NegativeActiveEnergyTotal).HasColumnName("negativeActiveEnergyTotal");
        builder.Property(m => m.ReactiveEnergyQuadrant1Total).HasColumnName("reactiveEnergyQuadrant1Total");
        builder.Property(m => m.ReactiveEnergyQuadrant3Total).HasColumnName("reactiveEnergyQuadrant3Total");
        builder.Property(m => m.TotalPower).HasColumnName("totalPower");
        builder.Property(m => m.CurrentPhase1).HasColumnName("currentPhase1");
        builder.Property(m => m.VoltagePhase1).HasColumnName("voltagePhase1");
        builder.Property(m => m.CurrentPhase2).HasColumnName("currentPhase2");
        builder.Property(m => m.VoltagePhase2).HasColumnName("voltagePhase2");
        builder.Property(m => m.CurrentPhase3).HasColumnName("currentPhase3");
        builder.Property(m => m.VoltagePhase3).HasColumnName("voltagePhase3");
        builder.Property(m => m.Uptime).HasColumnName("uptime");
        builder.Property(m => m.Timestamp).HasColumnName("timestamp");

        builder.Property(m => m.SmartMeterId)
            .HasConversion(
                v => v.Id,
                v => new SmartMeterId(v));
    }
}

public class MeasurementPerWeekConfiguration : IEntityTypeConfiguration<MeasurementPerWeek>
{
    public void Configure(EntityTypeBuilder<MeasurementPerWeek> builder)
    {
        builder.ToView("MeasurementPerWeek", "domain").HasNoKey();

        builder.Property(m => m.PositiveActivePower).HasColumnName("positiveActivePower");
        builder.Property(m => m.PositiveActiveEnergyTotal).HasColumnName("positiveActiveEnergyTotal");
        builder.Property(m => m.NegativeActivePower).HasColumnName("negativeActivePower");
        builder.Property(m => m.NegativeActiveEnergyTotal).HasColumnName("negativeActiveEnergyTotal");
        builder.Property(m => m.ReactiveEnergyQuadrant1Total).HasColumnName("reactiveEnergyQuadrant1Total");
        builder.Property(m => m.ReactiveEnergyQuadrant3Total).HasColumnName("reactiveEnergyQuadrant3Total");
        builder.Property(m => m.TotalPower).HasColumnName("totalPower");
        builder.Property(m => m.CurrentPhase1).HasColumnName("currentPhase1");
        builder.Property(m => m.VoltagePhase1).HasColumnName("voltagePhase1");
        builder.Property(m => m.CurrentPhase2).HasColumnName("currentPhase2");
        builder.Property(m => m.VoltagePhase2).HasColumnName("voltagePhase2");
        builder.Property(m => m.CurrentPhase3).HasColumnName("currentPhase3");
        builder.Property(m => m.VoltagePhase3).HasColumnName("voltagePhase3");
        builder.Property(m => m.Uptime).HasColumnName("uptime");
        builder.Property(m => m.Timestamp).HasColumnName("timestamp");

        builder.Property(m => m.SmartMeterId)
            .HasConversion(
                v => v.Id,
                v => new SmartMeterId(v));
    }
}