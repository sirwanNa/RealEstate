using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Article_Id_Language",
                schema: "re",
                table: "Article");

            migrationBuilder.AddColumn<string>(
                name: "UrlTitle",
                schema: "re",
                table: "Article",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Title_Language",
                schema: "re",
                table: "Article",
                columns: new[] { "Title", "Language" });

            migrationBuilder.CreateIndex(
                name: "IX_Article_UrlTitle_Language",
                schema: "re",
                table: "Article",
                columns: new[] { "UrlTitle", "Language" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Article_Title_Language",
                schema: "re",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_UrlTitle_Language",
                schema: "re",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "UrlTitle",
                schema: "re",
                table: "Article");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Id_Language",
                schema: "re",
                table: "Article",
                columns: new[] { "Id", "Language" });
        }
    }
}
