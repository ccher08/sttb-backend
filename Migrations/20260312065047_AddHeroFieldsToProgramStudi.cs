using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SttbApi.Migrations
{
    /// <inheritdoc />
    public partial class AddHeroFieldsToProgramStudi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "ProgramStudi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeroSubtitle",
                table: "ProgramStudi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeroTitle",
                table: "ProgramStudi",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                table: "ProgramStudi");

            migrationBuilder.DropColumn(
                name: "HeroSubtitle",
                table: "ProgramStudi");

            migrationBuilder.DropColumn(
                name: "HeroTitle",
                table: "ProgramStudi");
        }
    }
}
