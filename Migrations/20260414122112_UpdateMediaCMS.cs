using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SttbApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMediaCMS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Category_CategoryId",
                table: "Media");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Media",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Media_Slug",
                table: "Media",
                column: "Slug",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Category_CategoryId",
                table: "Media",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Category_CategoryId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_Slug",
                table: "Media");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Media",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Category_CategoryId",
                table: "Media",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
