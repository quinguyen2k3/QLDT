using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDT.Migrations
{
    /// <inheritdoc />
    public partial class AddMajor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Qualifications_QuaId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Qualifications_QuaId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.RenameColumn(
                name: "QuaId",
                table: "Employees",
                newName: "MajorId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_QuaId",
                table: "Employees",
                newName: "IX_Employees_MajorId");

            migrationBuilder.RenameColumn(
                name: "QuaId",
                table: "Classes",
                newName: "MajorId");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_QuaId",
                table: "Classes",
                newName: "IX_Classes_MajorId");

            migrationBuilder.CreateTable(
                name: "Major",
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
                    table.PrimaryKey("PK_Major", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Major_MajorId",
                table: "Classes",
                column: "MajorId",
                principalTable: "Major",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Major_MajorId",
                table: "Employees",
                column: "MajorId",
                principalTable: "Major",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Major_MajorId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Major_MajorId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Major");

            migrationBuilder.RenameColumn(
                name: "MajorId",
                table: "Employees",
                newName: "QuaId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_MajorId",
                table: "Employees",
                newName: "IX_Employees_QuaId");

            migrationBuilder.RenameColumn(
                name: "MajorId",
                table: "Classes",
                newName: "QuaId");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_MajorId",
                table: "Classes",
                newName: "IX_Classes_QuaId");

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.Id);
                });

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
    }
}
