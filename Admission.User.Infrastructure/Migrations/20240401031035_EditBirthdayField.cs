using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditBirthdayField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDay",
                table: "Applicants",
                newName: "Birthday");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "Applicants",
                newName: "BirthDay");
        }
    }
}
