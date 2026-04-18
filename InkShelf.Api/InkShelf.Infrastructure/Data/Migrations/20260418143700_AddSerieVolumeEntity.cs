using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InkShelf.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSerieVolumeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MangaTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MangaTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MangaThemes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MangaThemes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MangaSeries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MangaSeries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MangaPeoples",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MangaPeoples",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MangaCollections",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MangaCollections",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Editors",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Editors",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "SerieVolumes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VolumeNumber = table.Column<int>(type: "integer", nullable: false),
                    Synopsis = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    VFCoverPath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    VOCoverPath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    VOParutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VOParutionCountry = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VFParutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VFParutionCountry = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RecommendedAge = table.Column<int>(type: "integer", nullable: false),
                    EANCode = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    PriceCode = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    MangaSerieId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, computedColumnSql: "\"DeletedAt\" IS NOT NULL", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerieVolumes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SerieVolumes_MangaSeries_MangaSerieId",
                        column: x => x.MangaSerieId,
                        principalTable: "MangaSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SerieVolumes_MangaSerieId",
                table: "SerieVolumes",
                column: "MangaSerieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SerieVolumes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MangaTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MangaTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MangaThemes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MangaThemes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MangaSeries");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MangaSeries");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MangaPeoples");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MangaPeoples");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MangaCollections");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MangaCollections");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Editors");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Editors");
        }
    }
}
