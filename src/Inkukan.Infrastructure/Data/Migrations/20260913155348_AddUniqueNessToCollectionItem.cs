using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inkukan.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueNessToCollectionItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserListItems_UserId",
                table: "UserListItems");

            migrationBuilder.CreateIndex(
                name: "IX_UserListItems_UserId_VolumeId_Type",
                table: "UserListItems",
                columns: new[] { "UserId", "VolumeId", "Type" },
                unique: true,
                filter: "\"DeletedAt\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserListItems_UserId_VolumeId_Type",
                table: "UserListItems");

            migrationBuilder.CreateIndex(
                name: "IX_UserListItems_UserId",
                table: "UserListItems",
                column: "UserId");
        }
    }
}
