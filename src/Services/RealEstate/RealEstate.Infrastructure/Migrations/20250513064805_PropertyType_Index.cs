using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PropertyType_Index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyType_PropertyInventoryId",
                schema: "re",
                table: "PropertyType");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyType_PropertyInventoryId_RealEstateType",
                schema: "re",
                table: "PropertyType",
                columns: new[] { "PropertyInventoryId", "RealEstateType" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyType_PropertyInventoryId_RealEstateType",
                schema: "re",
                table: "PropertyType");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyType_PropertyInventoryId",
                schema: "re",
                table: "PropertyType",
                column: "PropertyInventoryId");
        }
    }
}
