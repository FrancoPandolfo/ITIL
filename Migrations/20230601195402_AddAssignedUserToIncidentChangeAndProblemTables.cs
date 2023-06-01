using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITIL.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignedUserToIncidentChangeAndProblemTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Change_ConfigurationItem_ConfigurationItemId",
                table: "Change");

            migrationBuilder.DropForeignKey(
                name: "FK_Incident_ConfigurationItem_ConfigurationItemId",
                table: "Incident");

            migrationBuilder.DropForeignKey(
                name: "FK_Problem_ConfigurationItem_ConfigurationItemId",
                table: "Problem");

            migrationBuilder.AlterColumn<int>(
                name: "ConfigurationItemId",
                table: "Problem",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "Problem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ConfigurationItemId",
                table: "Incident",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "Incident",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ConfigurationItemId",
                table: "Change",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "Change",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Problem_AssignedUserId",
                table: "Problem",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_AssignedUserId",
                table: "Incident",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Change_AssignedUserId",
                table: "Change",
                column: "AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Change_ConfigurationItem_ConfigurationItemId",
                table: "Change",
                column: "ConfigurationItemId",
                principalTable: "ConfigurationItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Change_User_AssignedUserId",
                table: "Change",
                column: "AssignedUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incident_ConfigurationItem_ConfigurationItemId",
                table: "Incident",
                column: "ConfigurationItemId",
                principalTable: "ConfigurationItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incident_User_AssignedUserId",
                table: "Incident",
                column: "AssignedUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Problem_ConfigurationItem_ConfigurationItemId",
                table: "Problem",
                column: "ConfigurationItemId",
                principalTable: "ConfigurationItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Problem_User_AssignedUserId",
                table: "Problem",
                column: "AssignedUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Change_ConfigurationItem_ConfigurationItemId",
                table: "Change");

            migrationBuilder.DropForeignKey(
                name: "FK_Change_User_AssignedUserId",
                table: "Change");

            migrationBuilder.DropForeignKey(
                name: "FK_Incident_ConfigurationItem_ConfigurationItemId",
                table: "Incident");

            migrationBuilder.DropForeignKey(
                name: "FK_Incident_User_AssignedUserId",
                table: "Incident");

            migrationBuilder.DropForeignKey(
                name: "FK_Problem_ConfigurationItem_ConfigurationItemId",
                table: "Problem");

            migrationBuilder.DropForeignKey(
                name: "FK_Problem_User_AssignedUserId",
                table: "Problem");

            migrationBuilder.DropIndex(
                name: "IX_Problem_AssignedUserId",
                table: "Problem");

            migrationBuilder.DropIndex(
                name: "IX_Incident_AssignedUserId",
                table: "Incident");

            migrationBuilder.DropIndex(
                name: "IX_Change_AssignedUserId",
                table: "Change");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "Problem");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "Incident");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "Change");

            migrationBuilder.AlterColumn<int>(
                name: "ConfigurationItemId",
                table: "Problem",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ConfigurationItemId",
                table: "Incident",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ConfigurationItemId",
                table: "Change",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Change_ConfigurationItem_ConfigurationItemId",
                table: "Change",
                column: "ConfigurationItemId",
                principalTable: "ConfigurationItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Incident_ConfigurationItem_ConfigurationItemId",
                table: "Incident",
                column: "ConfigurationItemId",
                principalTable: "ConfigurationItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Problem_ConfigurationItem_ConfigurationItemId",
                table: "Problem",
                column: "ConfigurationItemId",
                principalTable: "ConfigurationItem",
                principalColumn: "Id");
        }
    }
}
