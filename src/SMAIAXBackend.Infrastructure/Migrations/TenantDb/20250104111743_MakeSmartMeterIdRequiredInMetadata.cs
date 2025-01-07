using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMAIAXBackend.Infrastructure.Migrations.TenantDb
{
    /// <inheritdoc />
    public partial class MakeSmartMeterIdRequiredInMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_Metadata_SmartMeter_smartMeterId",
                schema: "domain",
                table: "Metadata");

            migrationBuilder.AlterColumn<Guid>(
                name: "smartMeterId",
                schema: "domain",
                table: "Metadata",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fK_Metadata_SmartMeter_smartMeterId",
                schema: "domain",
                table: "Metadata",
                column: "smartMeterId",
                principalSchema: "domain",
                principalTable: "SmartMeter",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_Metadata_SmartMeter_smartMeterId",
                schema: "domain",
                table: "Metadata");

            migrationBuilder.AlterColumn<Guid>(
                name: "smartMeterId",
                schema: "domain",
                table: "Metadata",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "fK_Metadata_SmartMeter_smartMeterId",
                schema: "domain",
                table: "Metadata",
                column: "smartMeterId",
                principalSchema: "domain",
                principalTable: "SmartMeter",
                principalColumn: "id");
        }
    }
}