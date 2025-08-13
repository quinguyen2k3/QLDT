using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDT.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCreditHourse2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CreditHourses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CreditHourses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CreditHourses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "CreditHourses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "CreditHourses",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CreditHourses");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CreditHourses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CreditHourses");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "CreditHourses");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "CreditHourses");
        }
    }
}
