using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InkShelf.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMangaSerie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Editors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MangaCollections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaCollections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MangaPeoples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    Firstname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaPeoples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MangaThemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaThemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MangaTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MangaSeries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TitleVF = table.Column<string>(type: "text", nullable: false),
                    TitleVO = table.Column<string>(type: "text", nullable: false),
                    TotalVolumes = table.Column<int>(type: "integer", nullable: false),
                    Synopsis = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DrawerId = table.Column<Guid>(type: "uuid", nullable: true),
                    TranslatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    EditorVFId = table.Column<Guid>(type: "uuid", nullable: true),
                    EditorVOId = table.Column<Guid>(type: "uuid", nullable: true),
                    CollectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaSeries", x => x.Id);
                    table.CheckConstraint("TotalVolumesShouldNotBeNegative", "\"TotalVolumes\" > -1");
                    table.ForeignKey(
                        name: "FK_MangaSeries_Editors_EditorVFId",
                        column: x => x.EditorVFId,
                        principalTable: "Editors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MangaSeries_Editors_EditorVOId",
                        column: x => x.EditorVOId,
                        principalTable: "Editors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MangaSeries_MangaCollections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "MangaCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MangaSeries_MangaPeoples_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "MangaPeoples",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MangaSeries_MangaPeoples_DrawerId",
                        column: x => x.DrawerId,
                        principalTable: "MangaPeoples",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MangaSeries_MangaPeoples_TranslatorId",
                        column: x => x.TranslatorId,
                        principalTable: "MangaPeoples",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MangaSeries_MangaTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "MangaTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MangaSerieMangaTheme",
                columns: table => new
                {
                    MangasId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThemesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaSerieMangaTheme", x => new { x.MangasId, x.ThemesId });
                    table.ForeignKey(
                        name: "FK_MangaSerieMangaTheme_MangaSeries_MangasId",
                        column: x => x.MangasId,
                        principalTable: "MangaSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MangaSerieMangaTheme_MangaThemes_ThemesId",
                        column: x => x.ThemesId,
                        principalTable: "MangaThemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Editors_Name",
                table: "Editors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MangaCollections_Code",
                table: "MangaCollections",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MangaSerieMangaTheme_ThemesId",
                table: "MangaSerieMangaTheme",
                column: "ThemesId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaSeries_AuthorId",
                table: "MangaSeries",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaSeries_CollectionId",
                table: "MangaSeries",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaSeries_DrawerId",
                table: "MangaSeries",
                column: "DrawerId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaSeries_EditorVFId",
                table: "MangaSeries",
                column: "EditorVFId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaSeries_EditorVOId",
                table: "MangaSeries",
                column: "EditorVOId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaSeries_TitleVF",
                table: "MangaSeries",
                column: "TitleVF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MangaSeries_TitleVO",
                table: "MangaSeries",
                column: "TitleVO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MangaSeries_TranslatorId",
                table: "MangaSeries",
                column: "TranslatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaSeries_TypeId",
                table: "MangaSeries",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaThemes_Code",
                table: "MangaThemes",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MangaSerieMangaTheme");

            migrationBuilder.DropTable(
                name: "MangaSeries");

            migrationBuilder.DropTable(
                name: "MangaThemes");

            migrationBuilder.DropTable(
                name: "Editors");

            migrationBuilder.DropTable(
                name: "MangaCollections");

            migrationBuilder.DropTable(
                name: "MangaPeoples");

            migrationBuilder.DropTable(
                name: "MangaTypes");
        }
    }
}
