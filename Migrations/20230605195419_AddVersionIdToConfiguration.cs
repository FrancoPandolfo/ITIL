using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITIL.Migrations
{
    /// <inheritdoc />
    public partial class AddVersionIdToConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VersionId",
                table: "ConfigurationItem",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VersionId",
                table: "ConfigurationItem");
        }
    }
}
