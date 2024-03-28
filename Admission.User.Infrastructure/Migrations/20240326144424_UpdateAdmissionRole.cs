using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission.User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdmissionRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "AspNetRoles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "AspNetRoles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedTime",
                table: "AspNetRoles",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "ModifiedTime",
                table: "AspNetRoles");
        }
    }
}
