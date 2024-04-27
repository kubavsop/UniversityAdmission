using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_Managers_ManagerId",
                table: "Applicants");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_ManagerId",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Applicants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "Applicants",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_ManagerId",
                table: "Applicants",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_Managers_ManagerId",
                table: "Applicants",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id");
        }
    }
}
