using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Admission.User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Type" },
                values: new object[,]
                {
                    { new Guid("00e96d45-dce1-4f4d-9891-83d3935f96ac"), null, "Admin", "ADMIN", 3 },
                    { new Guid("88fc0436-197e-465a-a71d-645b401941af"), null, "Manager", "MANAGER", 1 },
                    { new Guid("c6c4b1dd-7abb-496d-ab7b-537dd6d32ddd"), null, "Applicant", "APPLICANT", 0 },
                    { new Guid("dc7e0f08-14dc-481f-8ca8-fc896ef404a4"), null, "SeniorManager", "SENIORMANAGER", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00e96d45-dce1-4f4d-9891-83d3935f96ac"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("88fc0436-197e-465a-a71d-645b401941af"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c6c4b1dd-7abb-496d-ab7b-537dd6d32ddd"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dc7e0f08-14dc-481f-8ca8-fc896ef404a4"));
        }
    }
}
