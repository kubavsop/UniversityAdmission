using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixApplicant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentAdmissions_ApplicantId",
                table: "StudentAdmissions");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAdmissions_ApplicantId",
                table: "StudentAdmissions",
                column: "ApplicantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentAdmissions_ApplicantId",
                table: "StudentAdmissions");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAdmissions_ApplicantId",
                table: "StudentAdmissions",
                column: "ApplicantId",
                unique: true);
        }
    }
}
