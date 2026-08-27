using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inkukan.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserListItemIsLogicalDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "UserListItems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserListItems",
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
                table: "UserListItems");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "UserListItems");
        }
    }
}
