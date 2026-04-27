using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InkShelf.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeletionForAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "MangaTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "MangaThemes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "MangaSeries",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "MangaPeoples",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "MangaCollections",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Editors",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MangaTypes",
                type: "boolean",
                nullable: false,
                computedColumnSql: "\"DeletedAt\" IS NOT NULL",
                stored: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MangaThemes",
                type: "boolean",
                nullable: false,
                computedColumnSql: "\"DeletedAt\" IS NOT NULL",
                stored: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MangaSeries",
                type: "boolean",
                nullable: false,
                computedColumnSql: "\"DeletedAt\" IS NOT NULL",
                stored: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MangaPeoples",
                type: "boolean",
                nullable: false,
                computedColumnSql: "\"DeletedAt\" IS NOT NULL",
                stored: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MangaCollections",
                type: "boolean",
                nullable: false,
                computedColumnSql: "\"DeletedAt\" IS NOT NULL",
                stored: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Editors",
                type: "boolean",
                nullable: false,
                computedColumnSql: "\"DeletedAt\" IS NOT NULL",
                stored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MangaTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MangaThemes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MangaSeries");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MangaPeoples");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MangaCollections");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Editors");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MangaTypes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MangaThemes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MangaSeries");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MangaPeoples");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MangaCollections");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Editors");
        }
    }
}
