using System.Data;

using Microsoft.EntityFrameworkCore;

using Npgsql;

using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Enums;
using SMAIAXBackend.Domain.Model.ValueObjects.Ids;
using SMAIAXBackend.Domain.Repositories;
using SMAIAXBackend.Infrastructure.DbContexts;

namespace SMAIAXBackend.Infrastructure.Repositories;

public class MeasurementRepository(
    TenantDbContext tenantDbContext) : IMeasurementRepository
{
    public async Task<(List<Measurement>, int)> GetMeasurementsBySmartMeterAsync(SmartMeterId smartMeterId,
        DateTime? startAt,
        DateTime? endAt)
    {
        var query = tenantDbContext.Measurements.AsNoTracking()
            .Where(m => m.SmartMeterId.Equals(smartMeterId))
            .Where(m => startAt == null || m.Timestamp >= startAt)
            .Where(m => endAt == null || m.Timestamp <= endAt);

        return (await query.ToListAsync(), await query.CountAsync());
    }

    public async Task<(List<AggregatedMeasurement>, int)> GetMeasurementsBySmartMeterAndResolutionAsync(
        SmartMeterId smartMeterId,
        MeasurementResolution measurementResolution, DateTime? startAt, DateTime? endAt)
    {
        var tableName = GetTableName(measurementResolution);
        var sqlQuery = $"""
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

        await using var command = tenantDbContext.Database.GetDbConnection().CreateCommand();
        command.CommandText = sqlQuery;
        command.Parameters.Add(new NpgsqlParameter("@smartMeterId", smartMeterId.Id));
        command.Parameters.Add(new NpgsqlParameter("@startAt", NpgsqlTypes.NpgsqlDbType.TimestampTz)
        {
            Value = startAt ?? (object)DBNull.Value
        });
        command.Parameters.Add(new NpgsqlParameter("@endAt", NpgsqlTypes.NpgsqlDbType.TimestampTz)
        {
            Value = endAt ?? (object)DBNull.Value
        });
        await tenantDbContext.Database.OpenConnectionAsync();
        await using var result = await command.ExecuteReaderAsync();
        var aggregatedMeasurements = new List<AggregatedMeasurement>();
        while (await result.ReadAsync())
        {
            var measurement = new AggregatedMeasurement(
                // General
                uptime: result["uptime"]?.ToString() ?? string.Empty,
                timestamp: result.GetDateTime("timestamp"),

                // Positive Active Power Section
                minPositiveActivePower: result.GetDouble("minPositiveActivePower"),
                maxPositiveActivePower: result.GetDouble("maxPositiveActivePower"),
                avgPositiveActivePower: result.GetDouble("avgPositiveActivePower"),
                medPositiveActivePower: result.GetDouble("medPositiveActivePower"),

                // Positive Active Energy Total Section
                minPositiveActiveEnergyTotal: result.GetDouble("minPositiveActiveEnergyTotal"),
                maxPositiveActiveEnergyTotal: result.GetDouble("maxPositiveActiveEnergyTotal"),
                avgPositiveActiveEnergyTotal: result.GetDouble("avgPositiveActiveEnergyTotal"),
                medPositiveActiveEnergyTotal: result.GetDouble("medPositiveActiveEnergyTotal"),

                // Negative Active Power Section
                minNegativeActivePower: result.GetDouble("minNegativeActivePower"),
                maxNegativeActivePower: result.GetDouble("maxNegativeActivePower"),
                avgNegativeActivePower: result.GetDouble("avgNegativeActivePower"),
                medNegativeActivePower: result.GetDouble("medNegativeActivePower"),

                // Negative Active Energy Total Section
                minNegativeActiveEnergyTotal: result.GetDouble("minNegativeActiveEnergyTotal"),
                maxNegativeActiveEnergyTotal: result.GetDouble("maxNegativeActiveEnergyTotal"),
                avgNegativeActiveEnergyTotal: result.GetDouble("avgNegativeActiveEnergyTotal"),
                medNegativeActiveEnergyTotal: result.GetDouble("medNegativeActiveEnergyTotal"),

                // Reactive Energy Quadrant 1 Section
                minReactiveEnergyQuadrant1Total: result.GetDouble("minReactiveEnergyQuadrant1Total"),
                maxReactiveEnergyQuadrant1Total: result.GetDouble("maxReactiveEnergyQuadrant1Total"),
                avgReactiveEnergyQuadrant1Total: result.GetDouble("avgReactiveEnergyQuadrant1Total"),
                medReactiveEnergyQuadrant1Total: result.GetDouble("medReactiveEnergyQuadrant1Total"),

                // Reactive Energy Quadrant 3 Section
                minReactiveEnergyQuadrant3Total: result.GetDouble("minReactiveEnergyQuadrant3Total"),
                maxReactiveEnergyQuadrant3Total: result.GetDouble("maxReactiveEnergyQuadrant3Total"),
                avgReactiveEnergyQuadrant3Total: result.GetDouble("avgReactiveEnergyQuadrant3Total"),
                medReactiveEnergyQuadrant3Total: result.GetDouble("medReactiveEnergyQuadrant3Total"),

                // Total Power Section
                minTotalPower: result.GetDouble("minTotalPower"),
                maxTotalPower: result.GetDouble("maxTotalPower"),
                avgTotalPower: result.GetDouble("avgTotalPower"),
                medTotalPower: result.GetDouble("medTotalPower"),

                // Current Phase 1 Section
                minCurrentPhase1: result.GetDouble("minCurrentPhase1"),
                maxCurrentPhase1: result.GetDouble("maxCurrentPhase1"),
                avgCurrentPhase1: result.GetDouble("avgCurrentPhase1"),
                medCurrentPhase1: result.GetDouble("medCurrentPhase1"),

                // Voltage Phase 1 Section
                minVoltagePhase1: result.GetDouble("minVoltagePhase1"),
                maxVoltagePhase1: result.GetDouble("maxVoltagePhase1"),
                avgVoltagePhase1: result.GetDouble("avgVoltagePhase1"),
                medVoltagePhase1: result.GetDouble("medVoltagePhase1"),

                // Current Phase 2 Section
                minCurrentPhase2: result.GetDouble("minCurrentPhase2"),
                maxCurrentPhase2: result.GetDouble("maxCurrentPhase2"),
                avgCurrentPhase2: result.GetDouble("avgCurrentPhase2"),
                medCurrentPhase2: result.GetDouble("medCurrentPhase2"),

                // Voltage Phase 2 Section
                minVoltagePhase2: result.GetDouble("minVoltagePhase2"),
                maxVoltagePhase2: result.GetDouble("maxVoltagePhase2"),
                avgVoltagePhase2: result.GetDouble("avgVoltagePhase2"),
                medVoltagePhase2: result.GetDouble("medVoltagePhase2"),

                // Current Phase 3 Section
                minCurrentPhase3: result.GetDouble("minCurrentPhase3"),
                maxCurrentPhase3: result.GetDouble("maxCurrentPhase3"),
                avgCurrentPhase3: result.GetDouble("avgCurrentPhase3"),
                medCurrentPhase3: result.GetDouble("medCurrentPhase3"),

                // Voltage Phase 3 Section
                minVoltagePhase3: result.GetDouble("minVoltagePhase3"),
                maxVoltagePhase3: result.GetDouble("maxVoltagePhase3"),
                avgVoltagePhase3: result.GetDouble("avgVoltagePhase3"),
                medVoltagePhase3: result.GetDouble("medVoltagePhase3"),

                // Measurements
                amountOfMeasurements: result.GetInt32("amountOfMeasurements")
            );

            aggregatedMeasurements.Add(measurement);
        }

        await tenantDbContext.Database.CloseConnectionAsync();

        return (aggregatedMeasurements, aggregatedMeasurements.Count);
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
            default:
                throw new ArgumentException("Resolution RAW is not supported for this method.",
                    nameof(measurementResolution));
        }
    }
}