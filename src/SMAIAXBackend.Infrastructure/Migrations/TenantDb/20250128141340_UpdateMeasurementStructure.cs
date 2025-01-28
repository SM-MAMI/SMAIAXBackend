using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMAIAXBackend.Infrastructure.Migrations.TenantDb
{
    /// <inheritdoc />
    public partial class UpdateMeasurementStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Manually added drop because Views are dependent on Measurement.
            migrationBuilder.DropTable(
                name: "PolicyRequest",
                schema: "domain");

            migrationBuilder.DropColumn(
                name: "reactiveEnergyQuadrant1Total",
                schema: "domain",
                table: "Measurement");

            migrationBuilder.DropColumn(
                name: "uptime",
                schema: "domain",
                table: "Measurement");

            migrationBuilder.RenameColumn(
                name: "totalPower",
                schema: "domain",
                table: "Measurement",
                newName: "positiveReactiveEnergyTotal");

            migrationBuilder.RenameColumn(
                name: "reactiveEnergyQuadrant3Total",
                schema: "domain",
                table: "Measurement",
                newName: "negativeReactiveEnergyTotal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "positiveReactiveEnergyTotal",
                schema: "domain",
                table: "Measurement",
                newName: "totalPower");

            migrationBuilder.RenameColumn(
                name: "negativeReactiveEnergyTotal",
                schema: "domain",
                table: "Measurement",
                newName: "reactiveEnergyQuadrant3Total");

            migrationBuilder.AddColumn<double>(
                name: "reactiveEnergyQuadrant1Total",
                schema: "domain",
                table: "Measurement",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "uptime",
                schema: "domain",
                table: "Measurement",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PolicyRequest",
                schema: "domain",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    isAutomaticContractingEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    state = table.Column<string>(type: "text", nullable: false),
                    locationResolution = table.Column<string>(type: "text", nullable: false),
                    locations = table.Column<string>(type: "text", nullable: false),
                    maxHouseHoldSize = table.Column<int>(type: "integer", nullable: false),
                    maxPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    measurementResolution = table.Column<string>(type: "text", nullable: false),
                    minHouseHoldSize = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_PolicyRequest", x => x.id);
                });
        }
    }
}