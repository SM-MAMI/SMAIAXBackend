using Microsoft.EntityFrameworkCore;

using SMAIAXBackend.Domain.Model.Entities;
using SMAIAXBackend.Domain.Model.Enums;

namespace SMAIAXBackend.Infrastructure.Repositories.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<Measurement> FilterBy(this IQueryable<Measurement> query,
        MeasurementResolution measurementResolution)
    {
        switch (measurementResolution)
        {
            case MeasurementResolution.Minute:
                return query.Where(m => m.Timestamp.Second == 0);
            case MeasurementResolution.QuarterHour:
                return query.Where(m =>
                    (m.Timestamp.Minute == 0 || m.Timestamp.Minute == 15 || m.Timestamp.Minute == 30 ||
                     m.Timestamp.Minute == 45) && m.Timestamp.Second == 0);
            case MeasurementResolution.Hour:
                return query.Where(m => m.Timestamp.Minute == 0 && m.Timestamp.Second == 0);
            case MeasurementResolution.Day:
                return query.Where(m => m.Timestamp.Hour == 12 && m.Timestamp.Minute == 0 && m.Timestamp.Second == 0);
            case MeasurementResolution.Week:
                return query.Where(m =>
                    m.Timestamp.DayOfWeek == DayOfWeek.Wednesday && m.Timestamp.Hour == 12 && m.Timestamp.Minute == 0 &&
                    m.Timestamp.Second == 0);
            default:
                return query;
        }
    }

    public static IQueryable<Measurement> GroupTimestamps(this DbSet<Measurement> measurements,
        MeasurementResolution measurementResolution)
    {
        string sql = null;
        switch (measurementResolution)
        {
            case MeasurementResolution.Minute:
                sql = "SELECT time_bucket('1 minute', timestamp) AS \"timestamp\", ";
                break;
            case MeasurementResolution.QuarterHour:
                sql = "SELECT time_bucket('15 minutes', timestamp) AS \"timestamp\", ";
                break;
            case MeasurementResolution.Hour:
                sql = "SELECT time_bucket('1 hour', timestamp) AS \"timestamp\", ";
                break;
            case MeasurementResolution.Day:
                sql = "SELECT time_bucket('1 day', timestamp) AS \"timestamp\", ";
                break;
            case MeasurementResolution.Week:
                sql = "SELECT time_bucket('1 week', timestamp) AS \"timestamp\", ";
                break;
            default:
                return measurements;
        }

        sql +=
            """
            "smartMeterId",
            '' as "uptime",
            AVG("positiveActivePower") AS "positiveActivePower",
            AVG("positiveActiveEnergyTotal") AS "positiveActiveEnergyTotal",
            AVG("negativeActivePower") AS "negativeActivePower",
            AVG("negativeActiveEnergyTotal") AS "negativeActiveEnergyTotal",
            AVG("reactiveEnergyQuadrant1Total") AS "reactiveEnergyQuadrant1Total",
            AVG("reactiveEnergyQuadrant3Total") AS "reactiveEnergyQuadrant3Total",
            AVG("totalPower") AS "totalPower",
            AVG("currentPhase1") AS "currentPhase1",
            AVG("voltagePhase1") AS "voltagePhase1",
            AVG("currentPhase2") AS "currentPhase2",
            AVG("voltagePhase2") AS "voltagePhase2",
            AVG("currentPhase3") AS "currentPhase3",
            AVG("voltagePhase3") AS "voltagePhase3"
            FROM "domain"."Measurement"
            GROUP BY "timestamp", "smartMeterId";
            """;
        return measurements.FromSqlRaw(sql);
    }
}