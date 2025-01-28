using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMAIAXBackend.Infrastructure.Migrations.TenantDb
{
    /// <inheritdoc />
    public partial class RemoveDescSortOrderInMeasurementIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "iX_Measurement_timestamp",
                schema: "domain",
                table: "Measurement");

            migrationBuilder.CreateIndex(
                name: "iX_Measurement_timestamp",
                schema: "domain",
                table: "Measurement",
                column: "timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "iX_Measurement_timestamp",
                schema: "domain",
                table: "Measurement");

            migrationBuilder.CreateIndex(
                name: "iX_Measurement_timestamp",
                schema: "domain",
                table: "Measurement",
                column: "timestamp",
                descending: new bool[0]);
        }
    }
}