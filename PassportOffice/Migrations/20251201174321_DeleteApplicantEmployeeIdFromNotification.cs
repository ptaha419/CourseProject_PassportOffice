using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PassportOffice.Migrations
{
    /// <inheritdoc />
    public partial class DeleteApplicantEmployeeIdFromNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Applications",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_EmployeeId",
                table: "Applications",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Users_EmployeeId",
                table: "Applications",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Users_EmployeeId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_EmployeeId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Applications");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicantId",
                table: "Notifications",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Notifications",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ApplicantId",
                table: "Notifications",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_EmployeeId",
                table: "Notifications",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_ApplicantId",
                table: "Notifications",
                column: "ApplicantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_EmployeeId",
                table: "Notifications",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
