using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PassportOffice.Migrations
{
    /// <inheritdoc />
    public partial class DeleteServicesFromDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypesOfApplication_Departments_DepartmentId",
                table: "TypesOfApplication");

            migrationBuilder.DropIndex(
                name: "IX_TypesOfApplication_DepartmentId",
                table: "TypesOfApplication");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "TypesOfApplication");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "TypesOfApplication",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypesOfApplication_DepartmentId",
                table: "TypesOfApplication",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypesOfApplication_Departments_DepartmentId",
                table: "TypesOfApplication",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
