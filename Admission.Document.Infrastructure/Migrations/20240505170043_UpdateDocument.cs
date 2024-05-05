using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.Document.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PlaceOfBirth",
                table: "Passports",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicantId",
                table: "Passports",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicantId",
                table: "EducationDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Passports_ApplicantId",
                table: "Passports",
                column: "ApplicantId");

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
                name: "FK_Passports_Applicants_ApplicantId",
                table: "Passports",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationDocuments_Applicants_ApplicantId",
                table: "EducationDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Passports_Applicants_ApplicantId",
                table: "Passports");

            migrationBuilder.DropIndex(
                name: "IX_Passports_ApplicantId",
                table: "Passports");

            migrationBuilder.DropIndex(
                name: "IX_EducationDocuments_ApplicantId",
                table: "EducationDocuments");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Passports");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "EducationDocuments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PlaceOfBirth",
                table: "Passports",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
