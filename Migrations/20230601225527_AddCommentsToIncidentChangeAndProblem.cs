using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITIL.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentsToIncidentChangeAndProblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "Comments",
                table: "Problem",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "Comments",
                table: "Incident",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "Comments",
                table: "Change",
                type: "text[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Problem");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Incident");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Change");
        }
    }
}
