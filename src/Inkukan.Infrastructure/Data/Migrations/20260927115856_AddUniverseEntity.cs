using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inkukan.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniverseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UniverseId",
                table: "MangaSeries",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Universes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, computedColumnSql: "\"DeletedAt\" IS NOT NULL", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MangaSeries_UniverseId",
                table: "MangaSeries",
                column: "UniverseId");

            migrationBuilder.CreateIndex(
                name: "IX_Universes_Code",
                table: "Universes",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MangaSeries_Universes_UniverseId",
                table: "MangaSeries",
                column: "UniverseId",
                principalTable: "Universes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MangaSeries_Universes_UniverseId",
                table: "MangaSeries");

            migrationBuilder.DropTable(
                name: "Universes");

            migrationBuilder.DropIndex(
                name: "IX_MangaSeries_UniverseId",
                table: "MangaSeries");

            migrationBuilder.DropColumn(
                name: "UniverseId",
                table: "MangaSeries");
        }
    }
}
