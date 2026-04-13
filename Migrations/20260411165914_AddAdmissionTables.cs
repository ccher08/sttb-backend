using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SttbApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmissionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdmissionPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramStudiId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdmissionPackages_ProgramStudi_ProgramStudiId",
                        column: x => x.ProgramStudiId,
                        principalTable: "ProgramStudi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdmissionItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionPackageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdmissionItems_AdmissionPackages_AdmissionPackageId",
                        column: x => x.AdmissionPackageId,
                        principalTable: "AdmissionPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionItems_AdmissionPackageId",
                table: "AdmissionItems",
                column: "AdmissionPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionPackages_ProgramStudiId",
                table: "AdmissionPackages",
                column: "ProgramStudiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdmissionItems");

            migrationBuilder.DropTable(
                name: "AdmissionPackages");
        }
    }
}
