using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace healt_Center.Migrations
{
    /// <inheritdoc />
    public partial class doctor_linkedin_profile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "doctor_linkedin_profile",
                table: "tbl__Doctor",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "doctor_linkedin_profile",
                table: "tbl__Doctor");
        }
    }
}
