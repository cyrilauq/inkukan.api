using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inkukan.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeUniverseCodeRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                WITH universes_without_code AS (
                    SELECT "Id", regexp_replace(replace(lower("Name"), ' ', '_'), '[^a-z0-9_]', '', 'g') AS "CodeFromName"
                    FROM "Universes"
                    WHERE "Code" IS NULL
                ),
                numbered_codes AS (
                    SELECT "Id", "CodeFromName", ROW_NUMBER() OVER (PARTITION BY "CodeFromName" ORDER BY "Id") AS "Occurrence"
                    FROM universes_without_code
                )
                UPDATE "Universes" AS universe
                SET "Code" = CASE
                    WHEN numbered_codes."CodeFromName" = ''
                        OR numbered_codes."Occurrence" > 1
                        OR EXISTS (SELECT 1 FROM "Universes" AS other_universe WHERE other_universe."Code" = numbered_codes."CodeFromName")
                    THEN concat_ws('_', NULLIF(numbered_codes."CodeFromName", ''), replace(universe."Id"::text, '-', ''))
                    ELSE numbered_codes."CodeFromName"
                END
                FROM numbered_codes
                WHERE universe."Id" = numbered_codes."Id";
                """);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Universes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Universes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
