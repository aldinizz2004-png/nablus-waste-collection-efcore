using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NablusWS.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Municipalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WasteCollectionSites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MunicipalityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteCollectionSites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WasteCollectionSites_Municipalities_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Municipalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_WasteCollectionSites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "WasteCollectionSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiteTranslations",
                columns: table => new
                {
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteTranslations", x => new { x.SiteId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_SiteTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiteTranslations_WasteCollectionSites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "WasteCollectionSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "ar", "العربية" },
                    { 2, "en", "English" }
                });

            migrationBuilder.InsertData(
                table: "Municipalities",
                columns: new[] { "Id", "Address", "Name", "Phone" },
                values: new object[] { 1, "Nablus, Palestine", "Nablus Municipality", "09-2384000" });

            migrationBuilder.InsertData(
                table: "WasteCollectionSites",
                columns: new[] { "Id", "Address", "Code", "MunicipalityId" },
                values: new object[,]
                {
                    { 1, "Rafidia, Nablus", "NBL-RAF", 1 },
                    { 2, "Al-Makhfiya, Nablus", "NBL-MAK", 1 },
                    { 3, "Old City, Nablus", "NBL-OLD", 1 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FullName", "Phone", "SiteId" },
                values: new object[,]
                {
                    { 1, "Ahmad Khalil", "0599111111", 1 },
                    { 2, "Omar Nasser", "0599222222", 2 },
                    { 3, "Lina Saleh", "0599333333", 3 }
                });

            migrationBuilder.InsertData(
                table: "SiteTranslations",
                columns: new[] { "LanguageId", "SiteId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "موقع رفيديا لجمع النفايات" },
                    { 2, 1, "Rafidia Waste Collection Site" },
                    { 1, 2, "موقع المخفية لجمع النفايات" },
                    { 2, 2, "Al-Makhfiya Waste Collection Site" },
                    { 1, 3, "موقع البلدة القديمة لجمع النفايات" },
                    { 2, 3, "Old City Waste Collection Site" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SiteId",
                table: "Employees",
                column: "SiteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Code",
                table: "Languages",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SiteTranslations_LanguageId",
                table: "SiteTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteCollectionSites_Code",
                table: "WasteCollectionSites",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WasteCollectionSites_MunicipalityId",
                table: "WasteCollectionSites",
                column: "MunicipalityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "SiteTranslations");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "WasteCollectionSites");

            migrationBuilder.DropTable(
                name: "Municipalities");
        }
    }
}
