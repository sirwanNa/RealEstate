using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleDescription",
                schema: "re");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "re",
                table: "Article",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                schema: "re",
                table: "Article",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "re",
                table: "Article",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Id_Language",
                schema: "re",
                table: "Article",
                columns: new[] { "Id", "Language" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Article_Id_Language",
                schema: "re",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "re",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "Language",
                schema: "re",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "re",
                table: "Article");

            migrationBuilder.CreateTable(
                name: "ArticleDescription",
                schema: "re",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ArticleDescription_ArticleId_Language",
                schema: "re",
                table: "ArticleDescription",
                columns: new[] { "ArticleId", "Language" });
        }
    }
}
