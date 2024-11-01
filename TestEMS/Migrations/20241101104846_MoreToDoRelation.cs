using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestEMS.Migrations
{
    /// <inheritdoc />
    public partial class MoreToDoRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "ToDo",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AssignedTo",
                table: "ToDo",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDo_AssignedTo",
                table: "ToDo",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_ToDo_CreatedBy",
                table: "ToDo",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDo_EmployeeData_AssignedTo",
                table: "ToDo",
                column: "AssignedTo",
                principalTable: "EmployeeData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDo_EmployeeData_CreatedBy",
                table: "ToDo",
                column: "CreatedBy",
                principalTable: "EmployeeData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDo_EmployeeData_AssignedTo",
                table: "ToDo");

            migrationBuilder.DropForeignKey(
                name: "FK_ToDo_EmployeeData_CreatedBy",
                table: "ToDo");

            migrationBuilder.DropIndex(
                name: "IX_ToDo_AssignedTo",
                table: "ToDo");

            migrationBuilder.DropIndex(
                name: "IX_ToDo_CreatedBy",
                table: "ToDo");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ToDo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssignedTo",
                table: "ToDo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
