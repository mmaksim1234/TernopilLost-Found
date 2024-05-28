using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server2MVC.Migrations
{
    /// <inheritdoc />
    public partial class phoneadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "phoneNum",
                table: "Advertismnet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phoneNum",
                table: "Advertismnet");
        }
    }
}
