using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SttbApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHighlights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LectureSystem",
                table: "ProgramStudi");

            migrationBuilder.DropColumn(
                name: "Requirements",
                table: "ProgramStudi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LectureSystem",
                table: "ProgramStudi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Requirements",
                table: "ProgramStudi",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
