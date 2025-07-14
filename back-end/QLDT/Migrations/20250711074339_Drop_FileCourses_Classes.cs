using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDT.Migrations
{
    /// <inheritdoc />
    public partial class Drop_FileCourses_Classes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider == "Microsoft.EntityFrameworkCore.SqlServer")
            {
                migrationBuilder.Sql(
                    @"IF OBJECT_ID(N'[dbo].[FileCourse]', N'U') IS NOT NULL DROP TABLE [dbo].[FileCourse];");
                migrationBuilder.Sql(
                    @"IF OBJECT_ID(N'[dbo].[FileClass]', N'U') IS NOT NULL DROP TABLE [dbo].[FileClass];");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileCourse",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    // thêm các cột cần thiết nếu rollback
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileCourse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileClass",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    // thêm các cột cần thiết nếu rollback
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileClass", x => x.Id);
                });
        }

    }
}
