using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyTag_PropertyInventoryId_TagId",
                schema: "re",
                table: "PropertyTag");

            migrationBuilder.DropIndex(
                name: "IX_PropertyInventoryTitle_PropertyInventoryId_Language",
                schema: "re",
                table: "PropertyInventoryTitle");

            migrationBuilder.DropIndex(
                name: "IX_PropertyInventoryTitle_Title_Language",
                schema: "re",
                table: "PropertyInventoryTitle");

            migrationBuilder.DropIndex(
                name: "IX_PropertyInventoryTitle_UrlTitle_Language",
                schema: "re",
                table: "PropertyInventoryTitle");

            migrationBuilder.DropIndex(
                name: "IX_ArticleTag_ArticleId_TagId",
                schema: "re",
                table: "ArticleTag");

            migrationBuilder.DropIndex(
                name: "IX_Article_Title_Language",
                schema: "re",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_UrlTitle_Language",
                schema: "re",
                table: "Article");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTag_PropertyInventoryId_TagId_Language",
                schema: "re",
                table: "PropertyTag",
                columns: new[] { "PropertyInventoryId", "TagId", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInventoryTitle_PropertyInventoryId_Language",
                schema: "re",
                table: "PropertyInventoryTitle",
                columns: new[] { "PropertyInventoryId", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInventoryTitle_Title_Language",
                schema: "re",
                table: "PropertyInventoryTitle",
                columns: new[] { "Title", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInventoryTitle_UrlTitle_Language",
                schema: "re",
                table: "PropertyInventoryTitle",
                columns: new[] { "UrlTitle", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTag_ArticleId_TagId_Language",
                schema: "re",
                table: "ArticleTag",
                columns: new[] { "ArticleId", "TagId", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Article_Title_Language",
                schema: "re",
                table: "Article",
                columns: new[] { "Title", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Article_UrlTitle_Language",
                schema: "re",
                table: "Article",
                columns: new[] { "UrlTitle", "Language" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyTag_PropertyInventoryId_TagId_Language",
                schema: "re",
                table: "PropertyTag");

            migrationBuilder.DropIndex(
                name: "IX_PropertyInventoryTitle_PropertyInventoryId_Language",
                schema: "re",
                table: "PropertyInventoryTitle");

            migrationBuilder.DropIndex(
                name: "IX_PropertyInventoryTitle_Title_Language",
                schema: "re",
                table: "PropertyInventoryTitle");

            migrationBuilder.DropIndex(
                name: "IX_PropertyInventoryTitle_UrlTitle_Language",
                schema: "re",
                table: "PropertyInventoryTitle");

            migrationBuilder.DropIndex(
                name: "IX_ArticleTag_ArticleId_TagId_Language",
                schema: "re",
                table: "ArticleTag");

            migrationBuilder.DropIndex(
                name: "IX_Article_Title_Language",
                schema: "re",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_UrlTitle_Language",
                schema: "re",
                table: "Article");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTag_PropertyInventoryId_TagId",
                schema: "re",
                table: "PropertyTag",
                columns: new[] { "PropertyInventoryId", "TagId" });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInventoryTitle_PropertyInventoryId_Language",
                schema: "re",
                table: "PropertyInventoryTitle",
                columns: new[] { "PropertyInventoryId", "Language" });

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

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTag_ArticleId_TagId",
                schema: "re",
                table: "ArticleTag",
                columns: new[] { "ArticleId", "TagId" });

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
    }
}
