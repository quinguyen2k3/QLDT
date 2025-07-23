using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDT.Migrations
{
    /// <inheritdoc />
    public partial class AddQualification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "QuaId",
                table: "Employees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "QuaId",
                table: "Classes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_QuaId",
                table: "Employees",
                column: "QuaId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_QuaId",
                table: "Classes",
                column: "QuaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Qualifications_QuaId",
                table: "Classes",
                column: "QuaId",
                principalTable: "Qualifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Qualifications_QuaId",
                table: "Employees",
                column: "QuaId",
                principalTable: "Qualifications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Qualifications_QuaId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Qualifications_QuaId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropIndex(
                name: "IX_Employees_QuaId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Classes_QuaId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "QuaId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "QuaId",
                table: "Classes");
        }
    }
}
