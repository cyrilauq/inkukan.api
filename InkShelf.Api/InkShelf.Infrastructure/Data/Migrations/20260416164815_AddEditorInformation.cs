using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InkShelf.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEditorInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ConstitutionDate",
                table: "Editors",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ContactMail",
                table: "Editors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Editors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Editors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Editors",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConstitutionDate",
                table: "Editors");

            migrationBuilder.DropColumn(
                name: "ContactMail",
                table: "Editors");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Editors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Editors");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Editors");
        }
    }
}
