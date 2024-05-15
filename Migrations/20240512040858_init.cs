using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace localinezationBackend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverArt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TranslationRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestorUserId = table.Column<int>(type: "int", nullable: false),
                    MediaId = table.Column<int>(type: "int", nullable: false),
                    RequestLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestDialogue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranslationRequests_MediaInfo_MediaId",
                        column: x => x.MediaId,
                        principalTable: "MediaInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestReference",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVideo = table.Column<bool>(type: "bit", nullable: false),
                    TranslationRequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestReference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestReference_TranslationRequests_TranslationRequestId",
                        column: x => x.TranslationRequestId,
                        principalTable: "TranslationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TranslationRequestId = table.Column<int>(type: "int", nullable: false),
                    TranslatorUserId = table.Column<int>(type: "nvarchar(max)", nullable: true),
                    TranslatedText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<int>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsGuest = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translations_TranslationRequests_TranslationRequestId",
                        column: x => x.TranslationRequestId,
                        principalTable: "TranslationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestReference_TranslationRequestId",
                table: "RequestReference",
                column: "TranslationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TranslationRequests_MediaId",
                table: "TranslationRequests",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_TranslationRequestId",
                table: "Translations",
                column: "TranslationRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestReference");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "TranslationRequests");

            migrationBuilder.DropTable(
                name: "MediaInfo");
        }
    }
}
