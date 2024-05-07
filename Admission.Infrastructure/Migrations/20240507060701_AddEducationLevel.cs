using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEducationLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EducationLevelId",
                table: "EducationDocumentTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EducationDocumentTypes_EducationLevelId",
                table: "EducationDocumentTypes",
                column: "EducationLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationDocumentTypes_EducationLevels_EducationLevelId",
                table: "EducationDocumentTypes",
                column: "EducationLevelId",
                principalTable: "EducationLevels",
                principalColumn: "ExternalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationDocumentTypes_EducationLevels_EducationLevelId",
                table: "EducationDocumentTypes");

            migrationBuilder.DropIndex(
                name: "IX_EducationDocumentTypes_EducationLevelId",
                table: "EducationDocumentTypes");

            migrationBuilder.DropColumn(
                name: "EducationLevelId",
                table: "EducationDocumentTypes");
        }
    }
}
