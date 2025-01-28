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
                           SELECT *
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
            #region General

            var timestamp = result.GetDateTime("timestamp");
            var amountOfMeasurements = result.GetInt32("amountOfMeasurements");

            #endregion

            #region Voltage1

            var minVoltage1 = result.GetDouble("minVoltagePhase1");
            var maxVoltage1 = result.GetDouble("maxVoltagePhase1");
            var avgVoltage1 = result.GetDouble("avgVoltagePhase1");
            var medVoltage1 = result.GetDouble("medVoltagePhase1");

            #endregion

            #region Voltage2

            var minVoltage2 = result.GetDouble("minVoltagePhase2");
            var maxVoltage2 = result.GetDouble("maxVoltagePhase2");
            var avgVoltage2 = result.GetDouble("avgVoltagePhase2");
            var medVoltage2 = result.GetDouble("medVoltagePhase2");

            #endregion

            #region Voltage3

            var minVoltage3 = result.GetDouble("minVoltagePhase3");
            var maxVoltage3 = result.GetDouble("maxVoltagePhase3");
            var avgVoltage3 = result.GetDouble("avgVoltagePhase3");
            var medVoltage3 = result.GetDouble("medVoltagePhase3");

            #endregion

            #region CurrentPhase1

            var minCurrentPhase1 = result.GetDouble("minCurrentPhase1");
            var maxCurrentPhase1 = result.GetDouble("maxCurrentPhase1");
            var avgCurrentPhase1 = result.GetDouble("avgCurrentPhase1");
            var medCurrentPhase1 = result.GetDouble("medCurrentPhase1");

            #endregion

            #region CurrentPhase2

            var minCurrentPhase2 = result.GetDouble("minCurrentPhase2");
            var maxCurrentPhase2 = result.GetDouble("maxCurrentPhase2");
            var avgCurrentPhase2 = result.GetDouble("avgCurrentPhase2");
            var medCurrentPhase2 = result.GetDouble("medCurrentPhase2");

            #endregion

            #region CurrentPhase3

            var minCurrentPhase3 = result.GetDouble("minCurrentPhase3");
            var maxCurrentPhase3 = result.GetDouble("maxCurrentPhase3");
            var avgCurrentPhase3 = result.GetDouble("avgCurrentPhase3");
            var medCurrentPhase3 = result.GetDouble("medCurrentPhase3");

            #endregion

            #region PositiveActivePower

            var minPositiveActivePower = result.GetDouble("minPositiveActivePower");
            var maxPositiveActivePower = result.GetDouble("maxPositiveActivePower");
            var avgPositiveActivePower = result.GetDouble("avgPositiveActivePower");
            var medPositiveActivePower = result.GetDouble("medPositiveActivePower");

            #endregion

            #region NegativeActivePower

            var minNegativeActivePower = result.GetDouble("minNegativeActivePower");
            var maxNegativeActivePower = result.GetDouble("maxNegativeActivePower");
            var avgNegativeActivePower = result.GetDouble("avgNegativeActivePower");
            var medNegativeActivePower = result.GetDouble("medNegativeActivePower");

            #endregion

            #region PositiveReactiveEnergyTotal

            var minPositiveReactiveEnergyTotal = result.GetDouble("minPositiveReactiveEnergyTotal");
            var maxPositiveReactiveEnergyTotal = result.GetDouble("maxPositiveReactiveEnergyTotal");
            var avgPositiveReactiveEnergyTotal = result.GetDouble("avgPositiveReactiveEnergyTotal");
            var medPositiveReactiveEnergyTotal = result.GetDouble("medPositiveReactiveEnergyTotal");

            #endregion

            #region NegativeReactiveEnergyTotal

            var minNegativeReactiveEnergyTotal = result.GetDouble("minNegativeReactiveEnergyTotal");
            var maxNegativeReactiveEnergyTotal = result.GetDouble("maxNegativeReactiveEnergyTotal");
            var avgNegativeReactiveEnergyTotal = result.GetDouble("avgNegativeReactiveEnergyTotal");
            var medNegativeReactiveEnergyTotal = result.GetDouble("medNegativeReactiveEnergyTotal");

            #endregion

            #region PositiveActiveEnergyTotal

            var minPositiveActiveEnergyTotal = result.GetDouble("minPositiveActiveEnergyTotal");
            var maxPositiveActiveEnergyTotal = result.GetDouble("maxPositiveActiveEnergyTotal");
            var avgPositiveActiveEnergyTotal = result.GetDouble("avgPositiveActiveEnergyTotal");
            var medPositiveActiveEnergyTotal = result.GetDouble("medPositiveActiveEnergyTotal");

            #endregion

            #region NegativeActiveEnergyTotal

            var minNegativeActiveEnergyTotal = result.GetDouble("minNegativeActiveEnergyTotal");
            var maxNegativeActiveEnergyTotal = result.GetDouble("maxNegativeActiveEnergyTotal");
            var avgNegativeActiveEnergyTotal = result.GetDouble("avgNegativeActiveEnergyTotal");
            var medNegativeActiveEnergyTotal = result.GetDouble("medNegativeActiveEnergyTotal");

            #endregion

            AggregatedMeasurement measurement = new(
                timestamp, amountOfMeasurements,
                minVoltage1, maxVoltage1, avgVoltage1, medVoltage1,
                minVoltage2, maxVoltage2, avgVoltage2, medVoltage2,
                minVoltage3, maxVoltage3, avgVoltage3, medVoltage3,
                minCurrentPhase1, maxCurrentPhase1, avgCurrentPhase1, medCurrentPhase1,
                minCurrentPhase2, maxCurrentPhase2, avgCurrentPhase2, medCurrentPhase2,
                minCurrentPhase3, maxCurrentPhase3, avgCurrentPhase3, medCurrentPhase3,
                minPositiveActivePower, maxPositiveActivePower, avgPositiveActivePower, medPositiveActivePower,
                minNegativeActivePower, maxNegativeActivePower, avgNegativeActivePower, medNegativeActivePower,
                minPositiveReactiveEnergyTotal, maxPositiveReactiveEnergyTotal, avgPositiveReactiveEnergyTotal, medPositiveReactiveEnergyTotal,
                minNegativeReactiveEnergyTotal, maxNegativeReactiveEnergyTotal, avgNegativeReactiveEnergyTotal, medNegativeReactiveEnergyTotal,
                minPositiveActiveEnergyTotal, maxPositiveActiveEnergyTotal, avgPositiveActiveEnergyTotal, medPositiveActiveEnergyTotal,
                minNegativeActiveEnergyTotal, maxNegativeActiveEnergyTotal, avgNegativeActiveEnergyTotal, medNegativeActiveEnergyTotal
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