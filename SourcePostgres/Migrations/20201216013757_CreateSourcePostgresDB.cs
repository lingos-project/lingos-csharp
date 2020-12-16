using Microsoft.EntityFrameworkCore.Migrations;

namespace SourcePostgres.Migrations
{
    public partial class CreateSourcePostgresDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "keys",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_keys", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "locales",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false),
                    required = table.Column<bool>(type: "boolean", nullable: false),
                    deprecated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_locales", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "scopes",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false),
                    deprecated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scopes", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "translations",
                columns: table => new
                {
                    key_name = table.Column<string>(type: "text", nullable: false),
                    locale_name = table.Column<string>(type: "text", nullable: false),
                    scope_name = table.Column<string>(type: "text", nullable: false),
                    variant = table.Column<string>(type: "text", nullable: false, defaultValue: "none"),
                    text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_translations", x => new { x.key_name, x.scope_name, x.locale_name, x.variant });
                    table.ForeignKey(
                        name: "fk_translations_keys_key_name",
                        column: x => x.key_name,
                        principalTable: "keys",
                        principalColumn: "name",
                        onUpdate: ReferentialAction.Cascade,
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_translations_locales_locale_name",
                        column: x => x.locale_name,
                        principalTable: "locales",
                        principalColumn: "name",
                        onUpdate: ReferentialAction.Cascade,
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_translations_scopes_scope_name",
                        column: x => x.scope_name,
                        principalTable: "scopes",
                        principalColumn: "name",
                        onUpdate: ReferentialAction.Cascade,
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_translations_locale_name",
                table: "translations",
                column: "locale_name");

            migrationBuilder.CreateIndex(
                name: "ix_translations_scope_name",
                table: "translations",
                column: "scope_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "translations");

            migrationBuilder.DropTable(
                name: "keys");

            migrationBuilder.DropTable(
                name: "locales");

            migrationBuilder.DropTable(
                name: "scopes");
        }
    }
}
