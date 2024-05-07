using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEducationDocumentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NextEducationLevels_Applicants_ApplicantId",
                table: "NextEducationLevels");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "NextEducationLevels",
                newName: "EducationDocumentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_NextEducationLevels_ApplicantId",
                table: "NextEducationLevels",
                newName: "IX_NextEducationLevels_EducationDocumentTypeId");

            migrationBuilder.CreateTable(
                name: "EducationDocumentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDocumentTypes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_NextEducationLevels_EducationDocumentTypes_EducationDocumen~",
                table: "NextEducationLevels",
                column: "EducationDocumentTypeId",
                principalTable: "EducationDocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NextEducationLevels_EducationDocumentTypes_EducationDocumen~",
                table: "NextEducationLevels");

            migrationBuilder.DropTable(
                name: "EducationDocumentTypes");

            migrationBuilder.RenameColumn(
                name: "EducationDocumentTypeId",
                table: "NextEducationLevels",
                newName: "ApplicantId");

            migrationBuilder.RenameIndex(
                name: "IX_NextEducationLevels_EducationDocumentTypeId",
                table: "NextEducationLevels",
                newName: "IX_NextEducationLevels_ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_NextEducationLevels_Applicants_ApplicantId",
                table: "NextEducationLevels",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
