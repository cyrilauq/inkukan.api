using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InkShelf.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCoverHashesToSerieVolumeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VFCoverHash",
                table: "SerieVolumes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VOCoverHash",
                table: "SerieVolumes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VFCoverHash",
                table: "SerieVolumes");

            migrationBuilder.DropColumn(
                name: "VOCoverHash",
                table: "SerieVolumes");
        }
    }
}
