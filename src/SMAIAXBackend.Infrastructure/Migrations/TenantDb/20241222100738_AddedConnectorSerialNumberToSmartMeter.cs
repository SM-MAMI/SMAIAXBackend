using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMAIAXBackend.Infrastructure.Migrations.TenantDb
{
    /// <inheritdoc />
    public partial class AddedConnectorSerialNumberToSmartMeter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_Metadata_SmartMeter_smartMeterId",
                schema: "domain",
                table: "Metadata");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "domain",
                table: "SmartMeter",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "connectorSerialNumber",
                schema: "domain",
                table: "SmartMeter",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_Metadata_SmartMeter_smartMeterId",
                schema: "domain",
                table: "Metadata");

            migrationBuilder.DropColumn(
                name: "connectorSerialNumber",
                schema: "domain",
                table: "SmartMeter");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "domain",
                table: "SmartMeter",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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
    }
}