using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMAIAXBackend.Infrastructure.Migrations.TenantDb
{
    /// <inheritdoc />
    public partial class CreateMaterializedViewsForMeasurements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // per minute
            migrationBuilder.Sql("""
                                 CREATE MATERIALIZED VIEW "domain"."MeasurementPerMinute"("timestamp", "smartMeterId","uptime",
                                 "positiveActivePower", 
                                 "positiveActiveEnergyTotal",
                                 "negativeActivePower", 
                                 "negativeActiveEnergyTotal", 
                                 "reactiveEnergyQuadrant1Total",
                                 "reactiveEnergyQuadrant3Total",
                                 "totalPower",
                                 "currentPhase1",
                                 "voltagePhase1", 
                                 "currentPhase2",
                                 "voltagePhase2", 
                                 "currentPhase3",
                                 "voltagePhase3")
                                 WITH (timescaledb.continuous) AS
                                 SELECT time_bucket('1 minute', "timestamp"), 
                                             "smartMeterId",
                                             MIN("uptime"),
                                             AVG("positiveActivePower"),
                                             AVG("positiveActiveEnergyTotal") ,
                                             AVG("negativeActivePower"),
                                             AVG("negativeActiveEnergyTotal"),
                                             AVG("reactiveEnergyQuadrant1Total"),
                                             AVG("reactiveEnergyQuadrant3Total"),
                                             AVG("totalPower"),
                                             AVG("currentPhase1"),
                                             AVG("voltagePhase1"),
                                             AVG("currentPhase2"),
                                             AVG("voltagePhase2"),
                                             AVG("currentPhase3"),
                                             AVG("voltagePhase3")
                                             FROM "domain"."Measurement"
                                             GROUP BY time_bucket('1 minute', "timestamp"), "smartMeterId";
                                 """, true);

            // start refreshing view every minute
            migrationBuilder.Sql("""
                                 SELECT add_continuous_aggregate_policy('"domain"."MeasurementPerMinute"',
                                 start_offset => null,
                                 end_offset => null,
                                 schedule_interval => INTERVAL '1 minute');
                                 """);

            // per quarter hour
            migrationBuilder.Sql("""
                                 CREATE MATERIALIZED VIEW "domain"."MeasurementPerQuarterHour"("timestamp", "smartMeterId","uptime",
                                 "positiveActivePower", 
                                 "positiveActiveEnergyTotal",
                                 "negativeActivePower", 
                                 "negativeActiveEnergyTotal", 
                                 "reactiveEnergyQuadrant1Total",
                                 "reactiveEnergyQuadrant3Total",
                                 "totalPower",
                                 "currentPhase1",
                                 "voltagePhase1", 
                                 "currentPhase2",
                                 "voltagePhase2", 
                                 "currentPhase3",
                                 "voltagePhase3")
                                 WITH (timescaledb.continuous) AS
                                 SELECT time_bucket('15 minutes', "timestamp"), 
                                             "smartMeterId",
                                             MIN("uptime"),
                                             AVG("positiveActivePower"),
                                             AVG("positiveActiveEnergyTotal") ,
                                             AVG("negativeActivePower"),
                                             AVG("negativeActiveEnergyTotal"),
                                             AVG("reactiveEnergyQuadrant1Total"),
                                             AVG("reactiveEnergyQuadrant3Total"),
                                             AVG("totalPower"),
                                             AVG("currentPhase1"),
                                             AVG("voltagePhase1"),
                                             AVG("currentPhase2"),
                                             AVG("voltagePhase2"),
                                             AVG("currentPhase3"),
                                             AVG("voltagePhase3")
                                             FROM "domain"."Measurement"
                                             GROUP BY time_bucket('15 minutes', "timestamp"), "smartMeterId";
                                 """, true);

            // start refreshing view every quarter-hour
            migrationBuilder.Sql("""
                                 SELECT add_continuous_aggregate_policy('"domain"."MeasurementPerQuarterHour"',
                                 start_offset => null,
                                 end_offset => null,
                                 schedule_interval => INTERVAL '15 minutes');
                                 """);

            // per hour
            migrationBuilder.Sql("""
                                 CREATE MATERIALIZED VIEW "domain"."MeasurementPerHour"("timestamp", "smartMeterId","uptime",
                                 "positiveActivePower", 
                                 "positiveActiveEnergyTotal",
                                 "negativeActivePower", 
                                 "negativeActiveEnergyTotal", 
                                 "reactiveEnergyQuadrant1Total",
                                 "reactiveEnergyQuadrant3Total",
                                 "totalPower",
                                 "currentPhase1",
                                 "voltagePhase1", 
                                 "currentPhase2",
                                 "voltagePhase2", 
                                 "currentPhase3",
                                 "voltagePhase3")
                                 WITH (timescaledb.continuous) AS
                                 SELECT time_bucket('1 hour', "timestamp"), 
                                             "smartMeterId",
                                             MIN("uptime"),
                                             AVG("positiveActivePower"),
                                             AVG("positiveActiveEnergyTotal") ,
                                             AVG("negativeActivePower"),
                                             AVG("negativeActiveEnergyTotal"),
                                             AVG("reactiveEnergyQuadrant1Total"),
                                             AVG("reactiveEnergyQuadrant3Total"),
                                             AVG("totalPower"),
                                             AVG("currentPhase1"),
                                             AVG("voltagePhase1"),
                                             AVG("currentPhase2"),
                                             AVG("voltagePhase2"),
                                             AVG("currentPhase3"),
                                             AVG("voltagePhase3")
                                             FROM "domain"."Measurement"
                                             GROUP BY time_bucket('1 hour', "timestamp"), "smartMeterId";
                                 """, true);

            // start refreshing view every hour
            migrationBuilder.Sql("""
                                 SELECT add_continuous_aggregate_policy('"domain"."MeasurementPerHour"',
                                 start_offset => null,
                                 end_offset => null,
                                 schedule_interval => INTERVAL '1 hour');
                                 """);

            // per day
            migrationBuilder.Sql("""
                                 CREATE MATERIALIZED VIEW "domain"."MeasurementPerDay"("timestamp", "smartMeterId","uptime",
                                 "positiveActivePower", 
                                 "positiveActiveEnergyTotal",
                                 "negativeActivePower", 
                                 "negativeActiveEnergyTotal", 
                                 "reactiveEnergyQuadrant1Total",
                                 "reactiveEnergyQuadrant3Total",
                                 "totalPower",
                                 "currentPhase1",
                                 "voltagePhase1", 
                                 "currentPhase2",
                                 "voltagePhase2", 
                                 "currentPhase3",
                                 "voltagePhase3")
                                 WITH (timescaledb.continuous) AS
                                 SELECT time_bucket('1 day', "timestamp"), 
                                             "smartMeterId",
                                             MIN("uptime"),
                                             AVG("positiveActivePower"),
                                             AVG("positiveActiveEnergyTotal") ,
                                             AVG("negativeActivePower"),
                                             AVG("negativeActiveEnergyTotal"),
                                             AVG("reactiveEnergyQuadrant1Total"),
                                             AVG("reactiveEnergyQuadrant3Total"),
                                             AVG("totalPower"),
                                             AVG("currentPhase1"),
                                             AVG("voltagePhase1"),
                                             AVG("currentPhase2"),
                                             AVG("voltagePhase2"),
                                             AVG("currentPhase3"),
                                             AVG("voltagePhase3")
                                             FROM "domain"."Measurement"
                                             GROUP BY time_bucket('1 day', "timestamp"), "smartMeterId";
                                 """, true);

            // start refreshing view every day
            migrationBuilder.Sql("""
                                 SELECT add_continuous_aggregate_policy('"domain"."MeasurementPerDay"',
                                 start_offset => null,
                                 end_offset => null,
                                 schedule_interval => INTERVAL '1 day');
                                 """);

            // per week
            migrationBuilder.Sql("""
                                 CREATE MATERIALIZED VIEW "domain"."MeasurementPerWeek"("timestamp", "smartMeterId","uptime",
                                 "positiveActivePower", 
                                 "positiveActiveEnergyTotal",
                                 "negativeActivePower", 
                                 "negativeActiveEnergyTotal", 
                                 "reactiveEnergyQuadrant1Total",
                                 "reactiveEnergyQuadrant3Total",
                                 "totalPower",
                                 "currentPhase1",
                                 "voltagePhase1", 
                                 "currentPhase2",
                                 "voltagePhase2", 
                                 "currentPhase3",
                                 "voltagePhase3")
                                 WITH (timescaledb.continuous) AS
                                 SELECT time_bucket('1 week', "timestamp"), 
                                             "smartMeterId",
                                             MIN("uptime"),
                                             AVG("positiveActivePower"),
                                             AVG("positiveActiveEnergyTotal") ,
                                             AVG("negativeActivePower"),
                                             AVG("negativeActiveEnergyTotal"),
                                             AVG("reactiveEnergyQuadrant1Total"),
                                             AVG("reactiveEnergyQuadrant3Total"),
                                             AVG("totalPower"),
                                             AVG("currentPhase1"),
                                             AVG("voltagePhase1"),
                                             AVG("currentPhase2"),
                                             AVG("voltagePhase2"),
                                             AVG("currentPhase3"),
                                             AVG("voltagePhase3")
                                             FROM "domain"."Measurement"
                                             GROUP BY time_bucket('1 week', "timestamp"), "smartMeterId";
                                 """, true);

            // start refreshing view every week
            migrationBuilder.Sql("""
                                 SELECT add_continuous_aggregate_policy('"domain"."MeasurementPerWeek"',
                                 start_offset => null,
                                 end_offset => null,
                                 schedule_interval => INTERVAL '1 week');
                                 """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                 DROP MATERIALIZED VIEW IF EXISTS
                                 "domain"."MeasurementPerMinute",
                                 "domain"."MeasurementPerQuarterHour",
                                 "domain"."MeasurementPerHour",
                                 "domain"."MeasurementPerHour",
                                 "domain"."MeasurementPerDay",
                                 "domain"."MeasurementPerWeek"
                                 RESTRICT
                                 """);
        }
    }
}