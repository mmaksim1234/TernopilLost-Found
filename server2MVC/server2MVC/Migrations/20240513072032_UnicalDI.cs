using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server2MVC.Migrations
{
    /// <inheritdoc />
    public partial class UnicalDI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnicalIdOfItem",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnicalIdOfItem",
                table: "User");
        }
    }
}
