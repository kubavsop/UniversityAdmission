using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.Document.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTptMappingStrategy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationDocuments_Applicants_ApplicantId",
                table: "EducationDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_EducationDocuments_EducationDocumentId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Passports_PassportId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Passports_Applicants_ApplicantId",
                table: "Passports");

            migrationBuilder.DropIndex(
                name: "IX_Passports_ApplicantId",
                table: "Passports");

            migrationBuilder.DropIndex(
                name: "IX_Files_EducationDocumentId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_EducationDocuments_ApplicantId",
                table: "EducationDocuments");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Passports");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Passports");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "Passports");

            migrationBuilder.DropColumn(
                name: "ModifiedTime",
                table: "Passports");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Passports");

            migrationBuilder.DropColumn(
                name: "EducationDocumentId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "EducationDocuments");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "EducationDocuments");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "EducationDocuments");

            migrationBuilder.DropColumn(
                name: "ModifiedTime",
                table: "EducationDocuments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "EducationDocuments");

            migrationBuilder.RenameColumn(
                name: "PassportId",
                table: "Files",
                newName: "DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_PassportId",
                table: "Files",
                newName: "IX_Files_DocumentId");

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ApplicantId",
                table: "Documents",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationDocuments_Documents_Id",
                table: "EducationDocuments",
                column: "Id",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Documents_DocumentId",
                table: "Files",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_Documents_Id",
                table: "Passports",
                column: "Id",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationDocuments_Documents_Id",
                table: "EducationDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Documents_DocumentId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Passports_Documents_Id",
                table: "Passports");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "Files",
                newName: "PassportId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_DocumentId",
                table: "Files",
                newName: "IX_Files_PassportId");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicantId",
                table: "Passports",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Passports",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "Passports",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedTime",
                table: "Passports",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Passports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationDocumentId",
                table: "Files",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicantId",
                table: "EducationDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "EducationDocuments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "EducationDocuments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedTime",
                table: "EducationDocuments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "EducationDocuments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Passports_ApplicantId",
                table: "Passports",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_EducationDocumentId",
                table: "Files",
                column: "EducationDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationDocuments_ApplicantId",
                table: "EducationDocuments",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationDocuments_Applicants_ApplicantId",
                table: "EducationDocuments",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_EducationDocuments_EducationDocumentId",
                table: "Files",
                column: "EducationDocumentId",
                principalTable: "EducationDocuments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Passports_PassportId",
                table: "Files",
                column: "PassportId",
                principalTable: "Passports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_Applicants_ApplicantId",
                table: "Passports",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
