using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDT.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCreditHourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Hour",
                table: "CreditHourses",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "CreditHourses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "CreditHourses");

            migrationBuilder.AlterColumn<int>(
                name: "Hour",
                table: "CreditHourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
