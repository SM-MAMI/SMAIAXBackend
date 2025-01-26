using System.Data;
using System.Data.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Npgsql;

using NpgsqlTypes;

using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Enums;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Infrastructure.Configurations;
using SMAIAXBackend.Infrastructure.DbContexts;

namespace SMAIAXBackend.Infrastructure.Repositories;

public class MeasurementRepository(
    TenantDbContext tenantDbContext,
    ITenantDbContextFactory tenantDbContextFactory,
    IOptions<DatabaseConfiguration> databaseConfigOptions) : IMeasurementRepository
{
    public async Task<int> GetMeasurementCountBySmartMeterAndResolution(SmartMeterId smartMeterId,
        MeasurementResolution measurementResolution, DateTime? startAt, DateTime? endAt, Tenant? tenant = null)
    {
        TenantDbContext tenantSpecificDbContext = tenant != null
            ? tenantDbContextFactory.CreateDbContext(tenant.DatabaseName,
                databaseConfigOptions.Value.SuperUsername, databaseConfigOptions.Value.SuperUserPassword)
            : tenantDbContext;

        await using var command = tenantSpecificDbContext.Database.GetDbConnection().CreateCommand();
        var tableName = GetTableName(measurementResolution);
        var sqlQuery = $"""
                        SELECT COUNT(*)
                        FROM "domain".{tableName}
                        WHERE "smartMeterId" = @smartMeterId AND (@startAt IS NULL OR "timestamp" >= @startAt) AND (@endAt IS NULL OR "timestamp" <= @endAt)
                        """;
        AssignToCommand(command, sqlQuery, smartMeterId, startAt, endAt);

        await tenantSpecificDbContext.Database.OpenConnectionAsync();

        var count = await command.ExecuteScalarAsync();

        await tenantSpecificDbContext.Database.CloseConnectionAsync();

        return (int)Convert.ToInt64(count);
    }

    public async Task<(List<Measurement>, int)> GetMeasurementsBySmartMeterAsync(SmartMeterId smartMeterId,
        DateTime? startAt,
        DateTime? endAt,
        Tenant? tenant = null)
    {
        TenantDbContext tenantSpecificDbContext = tenant != null
            ? tenantDbContextFactory.CreateDbContext(tenant.DatabaseName,
                databaseConfigOptions.Value.SuperUsername, databaseConfigOptions.Value.SuperUserPassword)
            : tenantDbContext;

        var query = tenantSpecificDbContext.Measurements.AsNoTracking()
            .Where(m => m.SmartMeterId.Equals(smartMeterId))
            .Where(m => startAt == null || m.Timestamp >= startAt)
            .Where(m => endAt == null || m.Timestamp <= endAt);

        return (await query.ToListAsync(), await query.CountAsync());
    }

    public async Task<(List<AggregatedMeasurement>, int)> GetAggregatedMeasurementsBySmartMeterAsync(
        SmartMeterId smartMeterId,
        MeasurementResolution measurementResolution, DateTime? startAt, DateTime? endAt, Tenant? tenant = null)
    {
        TenantDbContext tenantSpecificDbContext = tenant != null
            ? tenantDbContextFactory.CreateDbContext(tenant.DatabaseName,
                databaseConfigOptions.Value.SuperUsername, databaseConfigOptions.Value.SuperUserPassword)
            : tenantDbContext;

        string tableName = GetTableName(measurementResolution);
        string sqlQuery = $"""
                           SELECT
                           "timestamp",
                           "uptime",
                           "minPositiveActivePower",
                           "maxPositiveActivePower",
                           "avgPositiveActivePower",
                           "medPositiveActivePower",
                           "minPositiveActiveEnergyTotal",
                           "maxPositiveActiveEnergyTotal",
                           "avgPositiveActiveEnergyTotal",
                           "medPositiveActiveEnergyTotal",
                           "minNegativeActivePower",
                           "maxNegativeActivePower",
                           "avgNegativeActivePower",
                           "medNegativeActivePower",
                           "minNegativeActiveEnergyTotal",
                           "maxNegativeActiveEnergyTotal",
                           "avgNegativeActiveEnergyTotal",
                           "medNegativeActiveEnergyTotal",
                           "minReactiveEnergyQuadrant1Total",
                           "maxReactiveEnergyQuadrant1Total",
                           "avgReactiveEnergyQuadrant1Total",
                           "medReactiveEnergyQuadrant1Total",
                           "minReactiveEnergyQuadrant3Total",
                           "maxReactiveEnergyQuadrant3Total",
                           "avgReactiveEnergyQuadrant3Total",
                           "medReactiveEnergyQuadrant3Total",
                           "minTotalPower",
                           "maxTotalPower",
                           "avgTotalPower",
                           "medTotalPower",
                           "minCurrentPhase1",
                           "maxCurrentPhase1",
                           "avgCurrentPhase1",
                           "medCurrentPhase1",
                           "minVoltagePhase1",
                           "maxVoltagePhase1",
                           "avgVoltagePhase1",
                           "medVoltagePhase1",
                           "minCurrentPhase2",
                           "maxCurrentPhase2",
                           "avgCurrentPhase2",
                           "medCurrentPhase2",
                           "minVoltagePhase2",
                           "maxVoltagePhase2",
                           "avgVoltagePhase2",
                           "medVoltagePhase2",
                           "minCurrentPhase3",
                           "maxCurrentPhase3",
                           "avgCurrentPhase3",
                           "medCurrentPhase3",
                           "minVoltagePhase3",
                           "maxVoltagePhase3",
                           "avgVoltagePhase3",
                           "medVoltagePhase3",
                           "amountOfMeasurements"
                           FROM "domain".{tableName}
                           WHERE "smartMeterId" = @smartMeterId AND (@startAt IS NULL OR "timestamp" >= @startAt) AND (@endAt IS NULL OR "timestamp" <= @endAt)
                           """;

        await using var command = tenantSpecificDbContext.Database.GetDbConnection().CreateCommand();
        AssignToCommand(command, sqlQuery, smartMeterId, startAt, endAt);

        await tenantSpecificDbContext.Database.OpenConnectionAsync();

        await using var result = await command.ExecuteReaderAsync();
        var aggregatedMeasurements = new List<AggregatedMeasurement>();
        var count = 0;
        while (await result.ReadAsync())
        {
            AggregatedMeasurement measurement = new AggregatedMeasurement(
                // General
                result["uptime"]?.ToString() ?? string.Empty,
                result.GetDateTime("timestamp"),

                // Positive Active Power Section
                result.GetDouble("minPositiveActivePower"),
                result.GetDouble("maxPositiveActivePower"),
                result.GetDouble("avgPositiveActivePower"),
                result.GetDouble("medPositiveActivePower"),

                // Positive Active Energy Total Section
                result.GetDouble("minPositiveActiveEnergyTotal"),
                result.GetDouble("maxPositiveActiveEnergyTotal"),
                result.GetDouble("avgPositiveActiveEnergyTotal"),
                result.GetDouble("medPositiveActiveEnergyTotal"),

                // Negative Active Power Section
                result.GetDouble("minNegativeActivePower"),
                result.GetDouble("maxNegativeActivePower"),
                result.GetDouble("avgNegativeActivePower"),
                result.GetDouble("medNegativeActivePower"),

                // Negative Active Energy Total Section
                result.GetDouble("minNegativeActiveEnergyTotal"),
                result.GetDouble("maxNegativeActiveEnergyTotal"),
                result.GetDouble("avgNegativeActiveEnergyTotal"),
                result.GetDouble("medNegativeActiveEnergyTotal"),

                // Reactive Energy Quadrant 1 Section
                result.GetDouble("minReactiveEnergyQuadrant1Total"),
                result.GetDouble("maxReactiveEnergyQuadrant1Total"),
                result.GetDouble("avgReactiveEnergyQuadrant1Total"),
                result.GetDouble("medReactiveEnergyQuadrant1Total"),

                // Reactive Energy Quadrant 3 Section
                result.GetDouble("minReactiveEnergyQuadrant3Total"),
                result.GetDouble("maxReactiveEnergyQuadrant3Total"),
                result.GetDouble("avgReactiveEnergyQuadrant3Total"),
                result.GetDouble("medReactiveEnergyQuadrant3Total"),

                // Total Power Section
                result.GetDouble("minTotalPower"),
                result.GetDouble("maxTotalPower"),
                result.GetDouble("avgTotalPower"),
                result.GetDouble("medTotalPower"),

                // Current Phase 1 Section
                result.GetDouble("minCurrentPhase1"),
                result.GetDouble("maxCurrentPhase1"),
                result.GetDouble("avgCurrentPhase1"),
                result.GetDouble("medCurrentPhase1"),

                // Voltage Phase 1 Section
                result.GetDouble("minVoltagePhase1"),
                result.GetDouble("maxVoltagePhase1"),
                result.GetDouble("avgVoltagePhase1"),
                result.GetDouble("medVoltagePhase1"),

                // Current Phase 2 Section
                result.GetDouble("minCurrentPhase2"),
                result.GetDouble("maxCurrentPhase2"),
                result.GetDouble("avgCurrentPhase2"),
                result.GetDouble("medCurrentPhase2"),

                // Voltage Phase 2 Section
                result.GetDouble("minVoltagePhase2"),
                result.GetDouble("maxVoltagePhase2"),
                result.GetDouble("avgVoltagePhase2"),
                result.GetDouble("medVoltagePhase2"),

                // Current Phase 3 Section
                result.GetDouble("minCurrentPhase3"),
                result.GetDouble("maxCurrentPhase3"),
                result.GetDouble("avgCurrentPhase3"),
                result.GetDouble("medCurrentPhase3"),

                // Voltage Phase 3 Section
                result.GetDouble("minVoltagePhase3"),
                result.GetDouble("maxVoltagePhase3"),
                result.GetDouble("avgVoltagePhase3"),
                result.GetDouble("medVoltagePhase3"),

                // Measurements
                result.GetInt32("amountOfMeasurements")
            );
            aggregatedMeasurements.Add(measurement);
            count += 1;
        }

        await tenantSpecificDbContext.Database.CloseConnectionAsync();

        return (aggregatedMeasurements, count);
    }

    private static string GetTableName(MeasurementResolution measurementResolution)
    {
        switch (measurementResolution)
        {
            case MeasurementResolution.Minute:
                return "\"MeasurementPerMinute\"";
            case MeasurementResolution.QuarterHour:
                return "\"MeasurementPerQuarterHour\"";
            case MeasurementResolution.Hour:
                return "\"MeasurementPerHour\"";
            case MeasurementResolution.Day:
                return "\"MeasurementPerDay\"";
            case MeasurementResolution.Week:
                return "\"MeasurementPerWeek\"";
            case MeasurementResolution.Raw:
                return "\"Measurement\"";
            default:
                throw new ArgumentException("Resolution RAW is not supported for this method.",
                    nameof(measurementResolution));
        }
    }

    private static void AssignToCommand(DbCommand command, string sqlQuery, SmartMeterId smartMeterId,
        DateTime? startAt,
        DateTime? endAt)
    {
        command.CommandText = sqlQuery;
        command.Parameters.Add(new NpgsqlParameter("@smartMeterId", smartMeterId.Id));
        command.Parameters.Add(new NpgsqlParameter("@startAt", NpgsqlDbType.TimestampTz)
        {
            Value = startAt ?? (object)DBNull.Value
        });
        command.Parameters.Add(new NpgsqlParameter("@endAt", NpgsqlDbType.TimestampTz)
        {
            Value = endAt ?? (object)DBNull.Value
        });
    }
}