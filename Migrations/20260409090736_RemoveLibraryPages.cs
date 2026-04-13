using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SttbApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLibraryPages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibraryPages");

            migrationBuilder.DropIndex(
                name: "IX_Libraries_Slug",
                table: "Libraries");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Libraries",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_Slug",
                table: "Libraries",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Libraries_Slug",
                table: "Libraries");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Libraries",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "LibraryPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryPages_Libraries_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Libraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_Slug",
                table: "Libraries",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LibraryPages_LibraryId",
                table: "LibraryPages",
                column: "LibraryId");
        }
    }
}
