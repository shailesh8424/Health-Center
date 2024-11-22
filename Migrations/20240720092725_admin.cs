using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace healt_Center.Migrations
{
    /// <inheritdoc />
    public partial class admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Branch_Discretion",
                table: "tbl_Branch",
                newName: "Branch_Description");

            migrationBuilder.AddColumn<string>(
                name: "Admin_Password",
                table: "tbl_admin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Admin_email",
                table: "tbl_admin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "admin_profile",
                table: "tbl_admin",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Admin_Password",
                table: "tbl_admin");

            migrationBuilder.DropColumn(
                name: "Admin_email",
                table: "tbl_admin");

            migrationBuilder.DropColumn(
                name: "admin_profile",
                table: "tbl_admin");

            migrationBuilder.RenameColumn(
                name: "Branch_Description",
                table: "tbl_Branch",
                newName: "Branch_Discretion");
        }
    }
}
