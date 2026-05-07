using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InkShelf.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Firstname = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Editors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ConstitutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    Website = table.Column<string>(type: "text", nullable: true),
                    ContactMail = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, computedColumnSql: "\"DeletedAt\" IS NOT NULL", stored: true)
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
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, computedColumnSql: "\"DeletedAt\" IS NOT NULL", stored: true)
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
                    Firstname = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, computedColumnSql: "\"DeletedAt\" IS NOT NULL", stored: true)
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
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, computedColumnSql: "\"DeletedAt\" IS NOT NULL", stored: true)
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
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, computedColumnSql: "\"DeletedAt\" IS NOT NULL", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, computedColumnSql: "\"DeletedAt\" IS NOT NULL", stored: true)
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

            migrationBuilder.CreateTable(
                name: "SerieVolumes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VolumeNumber = table.Column<int>(type: "integer", nullable: false),
                    Synopsis = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    VFCoverPath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    VFCoverHash = table.Column<string>(type: "text", nullable: true),
                    VOCoverPath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    VOCoverHash = table.Column<string>(type: "text", nullable: true),
                    VOParutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VOParutionCountry = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VFParutionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    VFParutionCountry = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    RecommendedAge = table.Column<int>(type: "integer", nullable: false),
                    EANCode = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    PriceCode = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    MangaSerieId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, computedColumnSql: "\"DeletedAt\" IS NOT NULL", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerieVolumes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SerieVolumes_MangaSeries_MangaSerieId",
                        column: x => x.MangaSerieId,
                        principalTable: "MangaSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

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
                name: "IX_MangaPeoples_Firstname_Lastname",
                table: "MangaPeoples",
                columns: new[] { "Firstname", "Lastname" },
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

            migrationBuilder.CreateIndex(
                name: "IX_SerieVolumes_MangaSerieId",
                table: "SerieVolumes",
                column: "MangaSerieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MangaSerieMangaTheme");

            migrationBuilder.DropTable(
                name: "SerieVolumes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MangaThemes");

            migrationBuilder.DropTable(
                name: "MangaSeries");

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
