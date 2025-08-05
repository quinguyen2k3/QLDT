using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDT.Migrations
{
    /// <inheritdoc />
    public partial class AddHourId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_EducationLevels_LevelId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Major_MajorId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditHourses_Classes_ClassId",
                table: "CreditHourses");

            migrationBuilder.DropIndex(
                name: "IX_CreditHourses_ClassId",
                table: "CreditHourses");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "CreditHourses");

            migrationBuilder.AddColumn<long>(
                name: "HourId",
                table: "Classes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_HourId",
                table: "Classes",
                column: "HourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_CreditHourses_HourId",
                table: "Classes",
                column: "HourId",
                principalTable: "CreditHourses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_EducationLevels_LevelId",
                table: "Classes",
                column: "LevelId",
                principalTable: "EducationLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Major_MajorId",
                table: "Classes",
                column: "MajorId",
                principalTable: "Major",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_CreditHourses_HourId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_EducationLevels_LevelId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Major_MajorId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_HourId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "HourId",
                table: "Classes");

            migrationBuilder.AddColumn<long>(
                name: "ClassId",
                table: "CreditHourses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CreditHourses_ClassId",
                table: "CreditHourses",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_EducationLevels_LevelId",
                table: "Classes",
                column: "LevelId",
                principalTable: "EducationLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Major_MajorId",
                table: "Classes",
                column: "MajorId",
                principalTable: "Major",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditHourses_Classes_ClassId",
                table: "CreditHourses",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
