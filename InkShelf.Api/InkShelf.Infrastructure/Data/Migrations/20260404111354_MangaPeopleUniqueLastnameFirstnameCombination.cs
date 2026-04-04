using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InkShelf.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MangaPeopleUniqueLastnameFirstnameCombination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MangaPeoples_Firstname_Lastname",
                table: "MangaPeoples",
                columns: new[] { "Firstname", "Lastname" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MangaPeoples_Firstname_Lastname",
                table: "MangaPeoples");
        }
    }
}
