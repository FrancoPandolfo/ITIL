using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITIL.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduledDateFieldToChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledDate",
                table: "Change",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduledDate",
                table: "Change");
        }
    }
}
