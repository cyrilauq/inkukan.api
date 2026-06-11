using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inkukan.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoveParutionCountryIntoMangaEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VFParutionCountry",
                table: "SerieVolumes");

            migrationBuilder.DropColumn(
                name: "VOParutionCountry",
                table: "SerieVolumes");

            migrationBuilder.AddColumn<string>(
                name: "VFParutionCountry",
                table: "MangaSeries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VOParutionCountry",
                table: "MangaSeries",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VFParutionCountry",
                table: "MangaSeries");

            migrationBuilder.DropColumn(
                name: "VOParutionCountry",
                table: "MangaSeries");

            migrationBuilder.AddColumn<string>(
                name: "VFParutionCountry",
                table: "SerieVolumes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VOParutionCountry",
                table: "SerieVolumes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
