using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server2MVC.Migrations
{
    /// <inheritdoc />
    public partial class CreateUSerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreateUserId",
                table: "Advertismnet",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "Advertismnet");
        }
    }
}
