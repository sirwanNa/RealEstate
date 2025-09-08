using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sixth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyInventoryTitle_PropertyInventoryId",
                schema: "re",
                table: "PropertyInventoryTitle");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInventoryTitle_PropertyInventoryId_Language",
                schema: "re",
                table: "PropertyInventoryTitle",
                columns: new[] { "PropertyInventoryId", "Language" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyInventoryTitle_PropertyInventoryId_Language",
                schema: "re",
                table: "PropertyInventoryTitle");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInventoryTitle_PropertyInventoryId",
                schema: "re",
                table: "PropertyInventoryTitle",
                column: "PropertyInventoryId");
        }
    }
}
