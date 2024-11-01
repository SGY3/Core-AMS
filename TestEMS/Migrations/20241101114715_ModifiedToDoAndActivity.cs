using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestEMS.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedToDoAndActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "MyActivity",
                columns: table => new
                {
                    ActivityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ToDoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActitivityTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ActivityTypeId = table.Column<int>(type: "int", nullable: false),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedTo = table.Column<int>(type: "int", nullable: true),
                    AssignedBy = table.Column<int>(type: "int", nullable: true),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyActivity", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_MyActivity_ActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityTypes",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyActivity_EmployeeData_AssignedBy",
                        column: x => x.AssignedBy,
                        principalTable: "EmployeeData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MyActivity_EmployeeData_AssignedTo",
                        column: x => x.AssignedTo,
                        principalTable: "EmployeeData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MyActivity_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyActivity_ActivityTypeId",
                table: "MyActivity",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MyActivity_AssignedBy",
                table: "MyActivity",
                column: "AssignedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MyActivity_AssignedTo",
                table: "MyActivity",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_MyActivity_ProjectId",
                table: "MyActivity",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyActivity");

            migrationBuilder.AlterColumn<int>(
                name: "ToDoId",
                table: "ToDo",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
