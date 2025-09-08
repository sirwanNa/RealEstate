using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyTag",
                schema: "re",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false),
                    PropertyInventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyTag_Constant_TagId",
                        column: x => x.TagId,
                        principalSchema: "re",
                        principalTable: "Constant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyTag_PropertyInventory_PropertyInventoryId",
                        column: x => x.PropertyInventoryId,
                        principalSchema: "re",
                        principalTable: "PropertyInventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTag_PropertyInventoryId_TagId",
                schema: "re",
                table: "PropertyTag",
                columns: new[] { "PropertyInventoryId", "TagId" });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTag_TagId",
                schema: "re",
                table: "PropertyTag",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyTag",
                schema: "re");
        }
    }
}
