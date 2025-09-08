using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlTitle",
                schema: "re",
                table: "PropertyInventoryTitle",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInventoryTitle_Title_Language",
                schema: "re",
                table: "PropertyInventoryTitle",
                columns: new[] { "Title", "Language" });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInventoryTitle_UrlTitle_Language",
                schema: "re",
                table: "PropertyInventoryTitle",
                columns: new[] { "UrlTitle", "Language" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyInventoryTitle_Title_Language",
                schema: "re",
                table: "PropertyInventoryTitle");

            migrationBuilder.DropIndex(
                name: "IX_PropertyInventoryTitle_UrlTitle_Language",
                schema: "re",
                table: "PropertyInventoryTitle");

            migrationBuilder.DropColumn(
                name: "UrlTitle",
                schema: "re",
                table: "PropertyInventoryTitle");
        }
    }
}
