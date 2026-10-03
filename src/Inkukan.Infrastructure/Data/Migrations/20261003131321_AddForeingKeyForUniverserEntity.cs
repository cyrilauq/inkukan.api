using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inkukan.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddForeingKeyForUniverserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MangaSeries_Universes_UniverseId",
                table: "MangaSeries");

            migrationBuilder.Sql("""
                INSERT INTO "Universes" ("Id", "Name", "Code", "CreatedAt", "UpdatedAt", "DeletedAt")
                SELECT gen_random_uuid(), series."TitleVF", 'serie_' || replace(series."Id"::text, '-', ''), now(), now(), NULL
                FROM "MangaSeries" AS series;

                UPDATE "MangaSeries" AS series
                SET "UniverseId" = universe."Id"
                FROM "Universes" AS universe
                WHERE universe."Code" = 'serie_' || replace(series."Id"::text, '-', '');
                """);

            migrationBuilder.AlterColumn<Guid>(
                name: "UniverseId",
                table: "MangaSeries",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MangaSeries_Universes_UniverseId",
                table: "MangaSeries",
                column: "UniverseId",
                principalTable: "Universes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MangaSeries_Universes_UniverseId",
                table: "MangaSeries");

            migrationBuilder.AlterColumn<Guid>(
                name: "UniverseId",
                table: "MangaSeries",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_MangaSeries_Universes_UniverseId",
                table: "MangaSeries",
                column: "UniverseId",
                principalTable: "Universes",
                principalColumn: "Id");
        }
    }
}
