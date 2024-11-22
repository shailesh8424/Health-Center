using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace healt_Center.Migrations
{
    /// <inheritdoc />
    public partial class branchhhhh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Branch_Discrepancy",
                table: "tbl_Branch",
                newName: "Branch_Logo");

            migrationBuilder.AddColumn<string>(
                name: "Branch_Discretion",
                table: "tbl_Branch",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Branch_Discretion",
                table: "tbl_Branch");

            migrationBuilder.RenameColumn(
                name: "Branch_Logo",
                table: "tbl_Branch",
                newName: "Branch_Discrepancy");
        }
    }
}
