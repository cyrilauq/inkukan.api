using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inkukan.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserListItemsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserListItem_AspNetUsers_UserId",
                table: "UserListItem");

            migrationBuilder.DropForeignKey(
                name: "FK_UserListItem_SerieVolumes_VolumeId",
                table: "UserListItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserListItem",
                table: "UserListItem");

            migrationBuilder.RenameTable(
                name: "UserListItem",
                newName: "UserListItems");

            migrationBuilder.RenameIndex(
                name: "IX_UserListItem_VolumeId",
                table: "UserListItems",
                newName: "IX_UserListItems_VolumeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserListItem_UserId",
                table: "UserListItems",
                newName: "IX_UserListItems_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserListItems",
                table: "UserListItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserListItems_AspNetUsers_UserId",
                table: "UserListItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserListItems_SerieVolumes_VolumeId",
                table: "UserListItems",
                column: "VolumeId",
                principalTable: "SerieVolumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserListItems_AspNetUsers_UserId",
                table: "UserListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_UserListItems_SerieVolumes_VolumeId",
                table: "UserListItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserListItems",
                table: "UserListItems");

            migrationBuilder.RenameTable(
                name: "UserListItems",
                newName: "UserListItem");

            migrationBuilder.RenameIndex(
                name: "IX_UserListItems_VolumeId",
                table: "UserListItem",
                newName: "IX_UserListItem_VolumeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserListItems_UserId",
                table: "UserListItem",
                newName: "IX_UserListItem_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserListItem",
                table: "UserListItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserListItem_AspNetUsers_UserId",
                table: "UserListItem",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserListItem_SerieVolumes_VolumeId",
                table: "UserListItem",
                column: "VolumeId",
                principalTable: "SerieVolumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
