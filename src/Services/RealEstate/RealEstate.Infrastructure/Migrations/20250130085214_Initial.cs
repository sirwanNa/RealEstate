using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "re");

            migrationBuilder.CreateTable(
                name: "Article",
                schema: "re",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Constant",
                schema: "re",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "re",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                schema: "re",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    RelatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleDescription",
                schema: "re",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<int>(type: "int", nullable: false),
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleDescription_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalSchema: "re",
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTag",
                schema: "re",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false),
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleTag_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalSchema: "re",
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleTag_Constant_TagId",
                        column: x => x.TagId,
                        principalSchema: "re",
                        principalTable: "Constant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConstantTitle",
                schema: "re",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Language = table.Column<int>(type: "int", nullable: false),
                    ConstantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstantTitle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConstantTitle_Constant_ConstantId",
                        column: x => x.ConstantId,
                        principalSchema: "re",
                        principalTable: "Constant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyInventory",
                schema: "re",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StructureType = table.Column<int>(type: "int", nullable: false),
                    RealEstateType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FinishDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    TotalOfRooms = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuilderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyInventory_Constant_BuilderId",
                        column: x => x.BuilderId,
                        principalSchema: "re",
                        principalTable: "Constant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyInventory_Constant_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "re",
                        principalTable: "Constant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyInventoryTitle",
                schema: "re",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyInventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentConditions = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyInventoryTitle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyInventoryTitle_PropertyInventory_PropertyInventoryId",
                        column: x => x.PropertyInventoryId,
                        principalSchema: "re",
                        principalTable: "PropertyInventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleDescription_ArticleId_Language",
                schema: "re",
                table: "ArticleDescription",
                columns: new[] { "ArticleId", "Language" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTag_ArticleId_TagId",
                schema: "re",
                table: "ArticleTag",
                columns: new[] { "ArticleId", "TagId" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTag_TagId",
                schema: "re",
                table: "ArticleTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstantTitle_ConstantId_Language",
                schema: "re",
                table: "ConstantTitle",
                columns: new[] { "ConstantId", "Language" });

            migrationBuilder.CreateIndex(
                name: "IX_Document_RelatedId_FileName",
                schema: "re",
                table: "Document",
                columns: new[] { "RelatedId", "FileName" });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInventory_BuilderId",
                schema: "re",
                table: "PropertyInventory",
                column: "BuilderId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInventory_RegionId",
                schema: "re",
                table: "PropertyInventory",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInventoryTitle_PropertyInventoryId",
                schema: "re",
                table: "PropertyInventoryTitle",
                column: "PropertyInventoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleDescription",
                schema: "re");

            migrationBuilder.DropTable(
                name: "ArticleTag",
                schema: "re");

            migrationBuilder.DropTable(
                name: "ConstantTitle",
                schema: "re");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "re");

            migrationBuilder.DropTable(
                name: "Document",
                schema: "re");

            migrationBuilder.DropTable(
                name: "PropertyInventoryTitle",
                schema: "re");

            migrationBuilder.DropTable(
                name: "Article",
                schema: "re");

            migrationBuilder.DropTable(
                name: "PropertyInventory",
                schema: "re");

            migrationBuilder.DropTable(
                name: "Constant",
                schema: "re");
        }
    }
}
