using System.Globalization;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Npgsql;

using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Enums;
using SMAIAXBackend.Domain.Model.ValueObjects;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Infrastructure.EntityConfigurations;

namespace SMAIAXBackend.Infrastructure.DbContexts;

public class TenantDbContext(DbContextOptions<TenantDbContext> options) : DbContext(options)
{
    public DbSet<SmartMeter> SmartMeters { get; init; }
    public DbSet<Measurement> Measurements { get; init; }
    public DbSet<Policy> Policies { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseCamelCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new MeasurementConfiguration());
        modelBuilder.ApplyConfiguration(new MetadataConfiguration());
        modelBuilder.ApplyConfiguration(new PolicyConfiguration());
        modelBuilder.ApplyConfiguration(new SmartMeterConfiguration());
    }

    public async Task SeedTestDataForJohnDoe()
    {
        SmartMeterId smartMeter1Id = new(Guid.Parse("070dec95-56bb-4154-a2c4-c26faf9fff4d"));
        ConnectorSerialNumber connectorSerialNumber1 = new(Guid.Parse("4ae9a3e1-1426-484c-a696-c26393a5b307"));
        Metadata metadata = Metadata.Create(new MetadataId(Guid.NewGuid()), DateTime.UtcNow.AddDays(-2),
            new Location("Hochschulstraße 1", "Dornbirn", "Vorarlberg", "Österreich", Continent.Europe),
            4, smartMeter1Id);
        SmartMeter smartMeter1 = SmartMeter.Create(smartMeter1Id, "Smart Meter 1", [metadata]);
        var policy = Policy.Create(new PolicyId(Guid.NewGuid()), "policy1", MeasurementResolution.Hour,
            LocationResolution.None, 100, smartMeter1Id);
        var policy2 = Policy.Create(new PolicyId(Guid.NewGuid()), "policy2", MeasurementResolution.Raw,
            LocationResolution.Continent, 1999, smartMeter1Id);

        SmartMeterId smartMeter2Id = new(Guid.Parse("74b243fd-188e-48a0-b5d1-4916f5464b0a"));
        SmartMeter smartMeter2 = SmartMeter.Create(smartMeter2Id, "Smart Meter 2", []);
        var policy3 = Policy.Create(new PolicyId(Guid.NewGuid()), "policy3", MeasurementResolution.Day,
            LocationResolution.StreetName, 999, smartMeter2Id);

        await Policies.AddAsync(policy);
        await Policies.AddAsync(policy2);
        await Policies.AddAsync(policy3);
        await SmartMeters.AddAsync(smartMeter1);
        await SmartMeters.AddAsync(smartMeter2);

        await InsertMeasurements(smartMeter1Id);
        await InsertMeasurements(smartMeter2Id);

        await SaveChangesAsync();
    }

    public async Task SeedTestDataForJaneDoe()
    {
        SmartMeterId smartMeter1Id = new(Guid.Parse("95cc68ed-c94e-40de-851b-b95aaacfb76c"));
        Metadata metadata = Metadata.Create(new MetadataId(Guid.NewGuid()), DateTime.UtcNow,
            new Location("Hochschulstraße 1", "Dornbirn", "Vorarlberg", "Österreich", Continent.Europe),
            2, smartMeter1Id);
        SmartMeter smartMeter1 = SmartMeter.Create(smartMeter1Id, "Smart Meter 1", [metadata]);
        var policy = Policy.Create(new PolicyId(Guid.NewGuid()), "policy1", MeasurementResolution.QuarterHour,
            LocationResolution.None, 100, smartMeter1Id);
        var policy2 = Policy.Create(new PolicyId(Guid.NewGuid()), "policy2", MeasurementResolution.Minute,
            LocationResolution.Continent, 2000, smartMeter1Id);

        SmartMeterId smartMeter2Id = new(Guid.Parse("1da14768-a0ce-432e-93b8-1b31e732c4af"));
        SmartMeter smartMeter2 = SmartMeter.Create(smartMeter2Id, "Smart Meter 2", []);
        var policy3 = Policy.Create(new PolicyId(Guid.NewGuid()), "policy3", MeasurementResolution.Week,
            LocationResolution.StreetName, 200, smartMeter2Id);

        await Policies.AddAsync(policy);
        await Policies.AddAsync(policy2);
        await Policies.AddAsync(policy3);
        await SmartMeters.AddAsync(smartMeter1);
        await SmartMeters.AddAsync(smartMeter2);

        await InsertMeasurements(smartMeter1Id);
        await InsertMeasurements(smartMeter2Id);

        await SaveChangesAsync();
    }

    public async Task SeedTestDataForMaxMustermann()
    {
        SmartMeterId smartMeter1Id = new(Guid.Parse("96ae137c-a687-4423-b7ff-4cb0b238bfc7"));
        Metadata metadata = Metadata.Create(new MetadataId(Guid.NewGuid()), DateTime.UtcNow,
            new Location("Hochschulstraße 1", "Dornbirn", "Vorarlberg", "Österreich", Continent.Europe),
            6, smartMeter1Id);
        SmartMeter smartMeter1 = SmartMeter.Create(smartMeter1Id, "Smart Meter 1", [metadata]);
        var policy = Policy.Create(new PolicyId(Guid.NewGuid()), "policy1", MeasurementResolution.Week,
            LocationResolution.None, 500, smartMeter1Id);
        var policy2 = Policy.Create(new PolicyId(Guid.NewGuid()), "policy2", MeasurementResolution.Raw,
            LocationResolution.Continent, 5000, smartMeter1Id);

        SmartMeterId smartMeter2Id = new(Guid.Parse("90b05cce-119f-40c4-9b28-e1d3088a109c"));
        SmartMeter smartMeter2 = SmartMeter.Create(smartMeter2Id, "Smart Meter 2", []);
        var policy3 = Policy.Create(new PolicyId(Guid.NewGuid()), "policy3", MeasurementResolution.Minute,
            LocationResolution.StreetName, 4000, smartMeter2Id);

        await Policies.AddAsync(policy);
        await Policies.AddAsync(policy2);
        await Policies.AddAsync(policy3);
        await SmartMeters.AddAsync(smartMeter1);
        await SmartMeters.AddAsync(smartMeter2);

        await InsertMeasurements(smartMeter1Id);
        await InsertMeasurements(smartMeter2Id);

        await SaveChangesAsync();
    }

    private async Task InsertMeasurements(SmartMeterId smartMeterId)
    {
        const int batchSize = 50;

        await Database.OpenConnectionAsync();
        const string sqlTemplate = @"
            INSERT INTO domain.""Measurement""(""positiveActivePower"", ""positiveActiveEnergyTotal"", ""negativeActivePower"", ""negativeActiveEnergyTotal"", ""reactiveEnergyQuadrant1Total"", ""reactiveEnergyQuadrant3Total"", ""totalPower"", ""currentPhase1"", ""voltagePhase1"", ""currentPhase2"", ""voltagePhase2"", ""currentPhase3"", ""voltagePhase3"", ""uptime"", ""timestamp"", ""smartMeterId"") 
            VALUES ({0});
        ";

        var random = new Random();
        var endDate = DateTime.UtcNow;
        var startDate = endDate.AddDays(-2);
        var interval = TimeSpan.FromSeconds(5);
        var insertStatements = new List<string>();

        for (var timestamp = startDate; timestamp <= endDate; timestamp += interval)
        {
            var values = $@"
                {random.Next(100, 200)}, 
                {random.Next(1000000, 2000000)}, 
                {random.Next(0, 10)}, 
                {random.Next(0, 10)}, 
                {random.Next(1000, 5000)}, 
                {random.Next(500000, 1000000)}, 
                {random.Next(100, 200)}, 
                {(random.NextDouble() * 10).ToString(CultureInfo.InvariantCulture)}, 
                {(random.NextDouble() * 100 + 200).ToString(CultureInfo.InvariantCulture)}, 
                {(random.NextDouble() * 10).ToString(CultureInfo.InvariantCulture)}, 
                {(random.NextDouble() * 100 + 200).ToString(CultureInfo.InvariantCulture)}, 
                {(random.NextDouble() * 10).ToString(CultureInfo.InvariantCulture)}, 
                {(random.NextDouble() * 100 + 200).ToString(CultureInfo.InvariantCulture)}, 
                '{timestamp.Subtract(startDate):dd\.hh\:mm\:ss}', 
                '{timestamp:O}', 
                '{smartMeterId.Id}'
            ";

            insertStatements.Add(string.Format(sqlTemplate, values));
        }

        await using var insertCommand = Database.GetDbConnection().CreateCommand();
        int i;
        for (i = 0; i < insertStatements.Count; i += 100)
        {
            var batch = insertStatements.Skip(i).Take(batchSize);
            insertCommand.CommandText = string.Join(";", batch);
            await insertCommand.ExecuteNonQueryAsync();
        }

        // Execute the missing statements
        var missingStatements = insertStatements.Skip(i).ToList();
        if (missingStatements.Count > 0)
        {
            insertCommand.CommandText = string.Join(";", missingStatements);
            await insertCommand.ExecuteNonQueryAsync();
        }

        await Database.CloseConnectionAsync();
    }
}