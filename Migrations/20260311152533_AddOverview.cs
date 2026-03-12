using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SttbApi.Migrations
{
    /// <inheritdoc />
    public partial class AddOverview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OverviewAbouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramStudiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverviewAbouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OverviewAbouts_ProgramStudi_ProgramStudiId",
                        column: x => x.ProgramStudiId,
                        principalTable: "ProgramStudi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OverviewRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramStudiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverviewRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OverviewRequirements_ProgramStudi_ProgramStudiId",
                        column: x => x.ProgramStudiId,
                        principalTable: "ProgramStudi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OverviewAbouts_ProgramStudiId",
                table: "OverviewAbouts",
                column: "ProgramStudiId");

            migrationBuilder.CreateIndex(
                name: "IX_OverviewRequirements_ProgramStudiId",
                table: "OverviewRequirements",
                column: "ProgramStudiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OverviewAbouts");

            migrationBuilder.DropTable(
                name: "OverviewRequirements");
        }
    }
}
