using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestEMS.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActitivityTitle",
                table: "MyActivity",
                newName: "ActivityTitle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivityTitle",
                table: "MyActivity",
                newName: "ActitivityTitle");
        }
    }
}
