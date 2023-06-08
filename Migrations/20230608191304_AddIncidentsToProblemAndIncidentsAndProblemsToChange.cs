using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITIL.Migrations
{
    /// <inheritdoc />
    public partial class AddIncidentsToProblemAndIncidentsAndProblemsToChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChangeId",
                table: "Problem",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChangeId",
                table: "Incident",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProblemId",
                table: "Incident",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Problem_ChangeId",
                table: "Problem",
                column: "ChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_ChangeId",
                table: "Incident",
                column: "ChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_ProblemId",
                table: "Incident",
                column: "ProblemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incident_Change_ChangeId",
                table: "Incident",
                column: "ChangeId",
                principalTable: "Change",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Incident_Problem_ProblemId",
                table: "Incident",
                column: "ProblemId",
                principalTable: "Problem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Problem_Change_ChangeId",
                table: "Problem",
                column: "ChangeId",
                principalTable: "Change",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incident_Change_ChangeId",
                table: "Incident");

            migrationBuilder.DropForeignKey(
                name: "FK_Incident_Problem_ProblemId",
                table: "Incident");

            migrationBuilder.DropForeignKey(
                name: "FK_Problem_Change_ChangeId",
                table: "Problem");

            migrationBuilder.DropIndex(
                name: "IX_Problem_ChangeId",
                table: "Problem");

            migrationBuilder.DropIndex(
                name: "IX_Incident_ChangeId",
                table: "Incident");

            migrationBuilder.DropIndex(
                name: "IX_Incident_ProblemId",
                table: "Incident");

            migrationBuilder.DropColumn(
                name: "ChangeId",
                table: "Problem");

            migrationBuilder.DropColumn(
                name: "ChangeId",
                table: "Incident");

            migrationBuilder.DropColumn(
                name: "ProblemId",
                table: "Incident");
        }
    }
}
