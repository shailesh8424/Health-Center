using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace healt_Center.Migrations
{
    /// <inheritdoc />
    public partial class doc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl__Doctor",
                columns: table => new
                {
                    doctor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doctor_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    doctor_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    doctor_number = table.Column<long>(type: "bigint", nullable: true),
                    doctor_department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    doctor_gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    doctor_profile = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl__Doctor", x => x.doctor_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    admin_message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    admin_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    Add_DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_patients_tbl_Departments_Add_DepartmentId",
                        column: x => x.Add_DepartmentId,
                        principalTable: "tbl_Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_patients_Add_DepartmentId",
                table: "patients",
                column: "Add_DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "tbl__Doctor");

            migrationBuilder.DropTable(
                name: "tbl_admin");

            migrationBuilder.DropTable(
                name: "tbl_Departments");
        }
    }
}
