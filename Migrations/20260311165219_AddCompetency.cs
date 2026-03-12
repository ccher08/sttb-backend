using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SttbApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCompetency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompetencyGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramStudiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencyGroups_ProgramStudi_ProgramStudiId",
                        column: x => x.ProgramStudiId,
                        principalTable: "ProgramStudi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompetencyGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencyItems_CompetencyGroups_CompetencyGroupId",
                        column: x => x.CompetencyGroupId,
                        principalTable: "CompetencyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyGroups_ProgramStudiId",
                table: "CompetencyGroups",
                column: "ProgramStudiId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyItems_CompetencyGroupId",
                table: "CompetencyItems",
                column: "CompetencyGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetencyItems");

            migrationBuilder.DropTable(
                name: "CompetencyGroups");
        }
    }
}
