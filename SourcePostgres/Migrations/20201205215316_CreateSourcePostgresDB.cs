using Microsoft.EntityFrameworkCore.Migrations;

namespace SourcePostgres.Migrations
{
    public partial class CreateSourcePostgresDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locales",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    Deprecated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locales", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Scopes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Deprecated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scopes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Key = table.Column<string>(type: "text", nullable: false),
                    LocaleName = table.Column<string>(type: "text", nullable: false),
                    ScopeName = table.Column<string>(type: "text", nullable: false),
                    Variant = table.Column<string>(type: "text", nullable: false, defaultValue: "none"),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => new { x.Key, x.LocaleName, x.ScopeName, x.Variant });
                    table.ForeignKey(
                        name: "FK_Translations_Locales_LocaleName",
                        column: x => x.LocaleName,
                        principalTable: "Locales",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Translations_Scopes_ScopeName",
                        column: x => x.ScopeName,
                        principalTable: "Scopes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Translations_LocaleName",
                table: "Translations",
                column: "LocaleName");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_ScopeName",
                table: "Translations",
                column: "ScopeName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "Locales");

            migrationBuilder.DropTable(
                name: "Scopes");
        }
    }
}
