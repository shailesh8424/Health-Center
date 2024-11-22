using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace healt_Center.Migrations
{
    /// <inheritdoc />
    public partial class addmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Admin_email",
                table: "tbl_admin",
                newName: "admin_email");

            migrationBuilder.RenameColumn(
                name: "Admin_Password",
                table: "tbl_admin",
                newName: "admin_Password");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "tbl_admin",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "tbl_admin",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "admin_email",
                table: "tbl_admin",
                newName: "Admin_email");

            migrationBuilder.RenameColumn(
                name: "admin_Password",
                table: "tbl_admin",
                newName: "Admin_Password");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "tbl_admin",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "tbl_admin",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
