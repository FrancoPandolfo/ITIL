using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITIL.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsTrackingNumberClosedDateLastUpdatedAndRootCauseToIncidentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedDate",
                table: "Incident",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Incident",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RootCause",
                table: "Incident",
                type: "text",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TrackingNumber",
                table: "Incident",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosedDate",
                table: "Incident");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Incident");

            migrationBuilder.DropColumn(
                name: "RootCause",
                table: "Incident");

            migrationBuilder.DropColumn(
                name: "TrackingNumber",
                table: "Incident");
        }
    }
}
