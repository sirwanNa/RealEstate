using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seventh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                schema: "re",
                table: "PropertyInventoryTitle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "TotalOfRooms",
                schema: "re",
                table: "PropertyInventory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Capacity",
                schema: "re",
                table: "PropertyInventory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DuringProjectPayment",
                schema: "re",
                table: "PropertyInventory",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HandoverPayment",
                schema: "re",
                table: "PropertyInventory",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prepayment",
                schema: "re",
                table: "PropertyInventory",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PropertyFeature",
                schema: "re",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureType = table.Column<int>(type: "int", nullable: false),
                    PropertyInventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyFeature_PropertyInventory_PropertyInventoryId",
                        column: x => x.PropertyInventoryId,
                        principalSchema: "re",
                        principalTable: "PropertyInventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyType",
                schema: "re",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RealEstateType = table.Column<int>(type: "int", nullable: false),
                    PropertyInventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyType_PropertyInventory_PropertyInventoryId",
                        column: x => x.PropertyInventoryId,
                        principalSchema: "re",
                        principalTable: "PropertyInventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeature_PropertyInventoryId",
                schema: "re",
                table: "PropertyFeature",
                column: "PropertyInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyType_PropertyInventoryId",
                schema: "re",
                table: "PropertyType",
                column: "PropertyInventoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyFeature",
                schema: "re");

            migrationBuilder.DropTable(
                name: "PropertyType",
                schema: "re");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                schema: "re",
                table: "PropertyInventoryTitle");

            migrationBuilder.DropColumn(
                name: "DuringProjectPayment",
                schema: "re",
                table: "PropertyInventory");

            migrationBuilder.DropColumn(
                name: "HandoverPayment",
                schema: "re",
                table: "PropertyInventory");

            migrationBuilder.DropColumn(
                name: "Prepayment",
                schema: "re",
                table: "PropertyInventory");

            migrationBuilder.AlterColumn<int>(
                name: "TotalOfRooms",
                schema: "re",
                table: "PropertyInventory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                schema: "re",
                table: "PropertyInventory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
