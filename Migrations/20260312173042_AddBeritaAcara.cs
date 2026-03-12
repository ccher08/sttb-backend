using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SttbApi.Migrations
{
    /// <inheritdoc />
    public partial class AddBeritaAcara : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Beritas",
                table: "Beritas");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Beritas");

            migrationBuilder.RenameTable(
                name: "Beritas",
                newName: "Berita");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Berita",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Berita",
                newName: "Excerpt");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Berita",
                newName: "Author");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Berita",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Berita",
                table: "Berita",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Acara",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acara", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acara");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Berita",
                table: "Berita");

            migrationBuilder.RenameTable(
                name: "Berita",
                newName: "Beritas");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Beritas",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "Excerpt",
                table: "Beritas",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Beritas",
                newName: "Content");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Beritas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Beritas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Beritas",
                table: "Beritas",
                column: "Id");
        }
    }
}
