using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inkukan.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCodeAndAgeSerie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PriceCode",
                table: "MangaSeries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedAge",
                table: "MangaSeries",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceCode",
                table: "MangaSeries");

            migrationBuilder.DropColumn(
                name: "RecommendedAge",
                table: "MangaSeries");
        }
    }
}
