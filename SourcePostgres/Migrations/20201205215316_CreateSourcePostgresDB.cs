using Microsoft.EntityFrameworkCore.Migrations;

namespace SourcePostgres.Migrations
{
    public partial class CreateSourcePostgresDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Locales",
                table => new
                {
                    Name = table.Column<string>("text", nullable: false),
                    Required = table.Column<bool>("boolean", nullable: false),
                    Deprecated = table.Column<bool>("boolean", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Locales", x => x.Name); });

            migrationBuilder.CreateTable(
                "Scopes",
                table => new
                {
                    Name = table.Column<string>("text", nullable: false),
                    Deprecated = table.Column<bool>("boolean", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Scopes", x => x.Name); });

            migrationBuilder.CreateTable(
                "Translations",
                table => new
                {
                    Key = table.Column<string>("text", nullable: false),
                    LocaleName = table.Column<string>("text", nullable: false),
                    ScopeName = table.Column<string>("text", nullable: false),
                    Variant = table.Column<string>("text", nullable: false, defaultValue: "none"),
                    Text = table.Column<string>("text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => new {x.Key, x.LocaleName, x.ScopeName, x.Variant});
                    table.ForeignKey(
                        "FK_Translations_Locales_LocaleName",
                        x => x.LocaleName,
                        "Locales",
                        "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Translations_Scopes_ScopeName",
                        x => x.ScopeName,
                        "Scopes",
                        "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Translations_LocaleName",
                "Translations",
                "LocaleName");

            migrationBuilder.CreateIndex(
                "IX_Translations_ScopeName",
                "Translations",
                "ScopeName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Translations");

            migrationBuilder.DropTable(
                "Locales");

            migrationBuilder.DropTable(
                "Scopes");
        }
    }
}