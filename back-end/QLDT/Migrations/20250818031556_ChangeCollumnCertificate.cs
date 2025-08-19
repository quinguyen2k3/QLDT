using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDT.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCollumnCertificate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileCertificates_Certificates_CetificateId",
                table: "FileCertificates");

            migrationBuilder.RenameColumn(
                name: "CetificateId",
                table: "FileCertificates",
                newName: "CertificateId");

            migrationBuilder.RenameIndex(
                name: "IX_FileCertificates_CetificateId",
                table: "FileCertificates",
                newName: "IX_FileCertificates_CertificateId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileCertificates_Certificates_CertificateId",
                table: "FileCertificates",
                column: "CertificateId",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileCertificates_Certificates_CertificateId",
                table: "FileCertificates");

            migrationBuilder.RenameColumn(
                name: "CertificateId",
                table: "FileCertificates",
                newName: "CetificateId");

            migrationBuilder.RenameIndex(
                name: "IX_FileCertificates_CertificateId",
                table: "FileCertificates",
                newName: "IX_FileCertificates_CetificateId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileCertificates_Certificates_CetificateId",
                table: "FileCertificates",
                column: "CetificateId",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
