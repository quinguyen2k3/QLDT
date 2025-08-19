using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDT.Migrations
{
    /// <inheritdoc />
    public partial class TableCetificate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cetificates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificateNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UnitId = table.Column<long>(type: "bigint", nullable: false),
                    EmpId = table.Column<long>(type: "bigint", nullable: false),
                    ClassId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cetificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cetificates_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cetificates_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cetificates_TrainingUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "TrainingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileCetificates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CetificateId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileCetificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileCetificates_Cetificates_CetificateId",
                        column: x => x.CetificateId,
                        principalTable: "Cetificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cetificates_ClassId",
                table: "Cetificates",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Cetificates_EmpId",
                table: "Cetificates",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Cetificates_UnitId",
                table: "Cetificates",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_FileCetificates_CetificateId",
                table: "FileCetificates",
                column: "CetificateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileCetificates");

            migrationBuilder.DropTable(
                name: "Cetificates");

            migrationBuilder.AddColumn<double>(
                name: "SoTinhChi",
                table: "Details",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
