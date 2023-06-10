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
            migrationBuilder.CreateTable(
                name: "ChangeIncident (Dictionary<string, object>)",
                columns: table => new
                {
                    ChangesId = table.Column<int>(type: "integer", nullable: false),
                    IncidentsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeIncident (Dictionary<string, object>)", x => new { x.ChangesId, x.IncidentsId });
                    table.ForeignKey(
                        name: "FK_ChangeIncident (Dictionary<string, object>)_Change_ChangesId",
                        column: x => x.ChangesId,
                        principalTable: "Change",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChangeIncident (Dictionary<string, object>)_Incident_Incide~",
                        column: x => x.IncidentsId,
                        principalTable: "Incident",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChangeProblem (Dictionary<string, object>)",
                columns: table => new
                {
                    ChangesId = table.Column<int>(type: "integer", nullable: false),
                    ProblemsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeProblem (Dictionary<string, object>)", x => new { x.ChangesId, x.ProblemsId });
                    table.ForeignKey(
                        name: "FK_ChangeProblem (Dictionary<string, object>)_Change_ChangesId",
                        column: x => x.ChangesId,
                        principalTable: "Change",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChangeProblem (Dictionary<string, object>)_Problem_Problems~",
                        column: x => x.ProblemsId,
                        principalTable: "Problem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncidentProblem (Dictionary<string, object>)",
                columns: table => new
                {
                    IncidentsId = table.Column<int>(type: "integer", nullable: false),
                    ProblemsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentProblem (Dictionary<string, object>)", x => new { x.IncidentsId, x.ProblemsId });
                    table.ForeignKey(
                        name: "FK_IncidentProblem (Dictionary<string, object>)_Incident_Incid~",
                        column: x => x.IncidentsId,
                        principalTable: "Incident",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IncidentProblem (Dictionary<string, object>)_Problem_Proble~",
                        column: x => x.ProblemsId,
                        principalTable: "Problem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChangeIncident (Dictionary<string, object>)_IncidentsId",
                table: "ChangeIncident (Dictionary<string, object>)",
                column: "IncidentsId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeProblem (Dictionary<string, object>)_ProblemsId",
                table: "ChangeProblem (Dictionary<string, object>)",
                column: "ProblemsId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentProblem (Dictionary<string, object>)_ProblemsId",
                table: "IncidentProblem (Dictionary<string, object>)",
                column: "ProblemsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeIncident (Dictionary<string, object>)");

            migrationBuilder.DropTable(
                name: "ChangeProblem (Dictionary<string, object>)");

            migrationBuilder.DropTable(
                name: "IncidentProblem (Dictionary<string, object>)");
        }
    }
}
