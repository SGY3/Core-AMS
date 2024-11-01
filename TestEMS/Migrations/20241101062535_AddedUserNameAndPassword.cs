using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestEMS.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserNameAndPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsActive",
                table: "EmployeeData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "EmployeeData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "EmployeeData");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "EmployeeData");
        }
    }
}
